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
        private readonly ILogger<ParentsController> logger;

        public ParentsController(IMapper mapper, IParentManager parentManager, ILogger<ParentsController> logger)
        {
            this.mapper = mapper;
            this.parentManager = parentManager;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ParentDetailsViewModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            return Ok(mapper.Map<List<ParentDetailsViewModel>>(parentManager.FindAll()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ParentDetailsViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateParentViewModel createParentViewModel)
        {
            var parent = await parentManager.Create(mapper.Map<Parent>(createParentViewModel));
            logger.LogInformation("Parent created: {@parent}", parent);
            return Ok(mapper.Map<ParentDetailsViewModel>(parent));
        }

        [HttpPut]
        [Route("{id:long}")]
        [ProducesResponseType(typeof(ParentDetailsViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateParentViewModel updateParentViewModel)
        {
            var parent = mapper.Map<Parent>(updateParentViewModel);
            parent.Id = id;
            await parentManager.Update(parent);
            logger.LogInformation("Parent updated: {@parent}", parent);
            return Ok(mapper.Map<ParentDetailsViewModel>(parent));
        }

        [HttpDelete]
        [Route("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(long id)
        {
            await parentManager.Remove(id);
            logger.LogInformation("Parent removed. ID: {id}", id);
            return Ok();
        }
    }
}
