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
    public class StudentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IStudentManager studentManager;

        public StudentsController(IMapper mapper, IStudentManager studentManager)
        {
            this.mapper = mapper;
            this.studentManager = studentManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<StudentViewModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            return Ok(mapper.Map<List<StudentViewModel>>(studentManager.FindAll()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(StudentViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] StudentViewModel StudentViewModel)
        {
            return Ok(mapper.Map<StudentViewModel>(await studentManager.Create(mapper.Map<Student>(StudentViewModel))));
        }

        [HttpPut]
        [ProducesResponseType(typeof(StudentViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] StudentViewModel StudentViewModel)
        {
            return Ok(mapper.Map<StudentViewModel>(await studentManager.Update(mapper.Map<Student>(StudentViewModel))));
        }

        [HttpDelete]
        [Route("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(long id)
        {
            await studentManager.Remove(id);
            return Ok();
        }

        [HttpGet]
        [Route("~/api/parents/{id:long}/students")]
        [ProducesResponseType(typeof(List<StudentViewModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetByParentId(long id)
        {
            return Ok(mapper.Map<List<StudentViewModel>>(studentManager.FindByParent(id)));
        }
    }
}
