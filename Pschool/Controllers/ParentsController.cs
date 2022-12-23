using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pschool.Contracts;
using Pschool.Shared.Models;
using Pschool.Shared.ViewModels.ParentViewModels;

namespace Pschool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IParentManager parentManager;

        public ParentsController(IMapper mapper, IParentManager parentManager)
        {
            this.mapper = mapper;
            this.parentManager = parentManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ParentViewModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            return Ok(mapper.Map<List<ParentViewModel>>(parentManager.FindAll()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ParentViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] ParentViewModel parentViewModel)
        {
            return Ok(mapper.Map<ParentViewModel>(await parentManager.Create(mapper.Map<Parent>(parentViewModel))));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ParentViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] ParentViewModel parentViewModel)
        {
            return Ok(mapper.Map<ParentViewModel>(await parentManager.Update(mapper.Map<Parent>(parentViewModel))));
        }

        [HttpDelete]
        [Route("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(long id)
        {
            await parentManager.Remove(id);
            return Ok();
        }

    }
}
