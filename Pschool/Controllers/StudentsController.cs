using System.Net;
using AutoMapper;
using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pschool.Contracts;
using Pschool.Shared.Models;
using Pschool.Shared.ViewModels.StudentViewModels;
using Pschool.ViewModels;

namespace Pschool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IStudentManager studentManager;
        private readonly AzureConfiguration azureConfiguration;
        private readonly ILogger<StudentsController> logger;

        public StudentsController(
            IMapper mapper,
            IStudentManager studentManager,
            IOptions<AzureConfiguration> options,
            ILogger<StudentsController> logger)
        {
            this.mapper = mapper;
            this.studentManager = studentManager;
            this.logger = logger;
            this.azureConfiguration = options.Value;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<StudentDetailsViewModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            return Ok(mapper.Map<List<StudentDetailsViewModel>>(studentManager.FindAll()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(StudentDetailsViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateStudentWithFile createStudentViewModel)
        {
            var student = mapper.Map<Student>(createStudentViewModel); ;

            var successResult = new Result<Student>(new Student());
            var failedResult = new Result<Student>(new ValidationException("", new List<ValidationFailure>()));

            if (createStudentViewModel.Document != null)
            {
                var fileName = Guid.NewGuid().ToString();
                var document = createStudentViewModel.Document;

                try
                {
                    StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(
                        azureConfiguration.AccountName,
                        azureConfiguration.AccountKey);
                    var blobServiceClient = new BlobServiceClient(
                        new Uri(azureConfiguration.BlobUri!),
                        sharedKeyCredential);
                    BlobContainerClient container = blobServiceClient.GetBlobContainerClient(azureConfiguration.ContainerName);

                    if (!await container.ExistsAsync())
                    {
                        container = await blobServiceClient.CreateBlobContainerAsync(azureConfiguration.ContainerName);
                    }
                    BlobClient blobClient = container.GetBlobClient(fileName);

                    using (var fileStream = document.OpenReadStream())
                    {
                        await blobClient.UploadAsync(fileStream, metadata: new Dictionary<string, string>()
                        {
                            {"email", student.Email! },
                            {"originalFileName", document.FileName}
                        });
                        student.Document = new Document() { FileName = fileName };
                    }
                }
                catch (RequestFailedException e)
                {
                    logger.LogError(e.Message);
                }
            }

            return Reply(await studentManager.Create(student), x =>
            {
                logger.LogInformation("Student created: {@student}", x);
                return mapper.Map<StudentDetailsViewModel>(x);
            });
        }

        [HttpPut]
        [Route("{id:long}")]
        [ProducesResponseType(typeof(StudentDetailsViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateStudentViewModel updateStudentViewModel)
        {
            var student = mapper.Map<Student>(updateStudentViewModel);
            student.Id = id;

            return Reply(await studentManager.Update(student), x =>
            {
                logger.LogInformation("Student updated: {@student}", x);
                return mapper.Map<StudentDetailsViewModel>(x);
            });
        }

        [HttpDelete]
        [Route("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(long id)
        {
            await studentManager.Remove(id);
            logger.LogInformation("Student removed. ID: {id}", id);
            return Ok();
        }

        [HttpGet]
        [Route("~/api/parents/{id:long}/students")]
        [ProducesResponseType(typeof(List<StudentDetailsViewModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetByParentId(long id)
        {
            return Ok(mapper.Map<List<StudentDetailsViewModel>>(studentManager.FindByParent(id)));
        }
    }
}
