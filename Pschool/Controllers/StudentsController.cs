using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pschool.Contracts;
using Pschool.Shared.Models;
using Pschool.Shared.ViewModels.StudentViewModels;

namespace Pschool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IStudentManager studentManager;
        private readonly ILogger<StudentsController> logger;

        public StudentsController(
            IMapper mapper,
            IStudentManager studentManager,
            ILogger<StudentsController> logger)
        {
            this.mapper = mapper;
            this.studentManager = studentManager;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<StudentDetailsViewModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            return Ok(mapper.Map<List<StudentDetailsViewModel>>(studentManager.FindAll()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(StudentDetailsViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateStudentViewModel createStudentViewModel)
        {
            var student = mapper.Map<Student>(createStudentViewModel); ;

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
