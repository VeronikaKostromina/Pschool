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
    public class ParentsController : BaseController
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
            var parent = mapper.Map<Parent>(createParentViewModel);

            return Reply(await parentManager.Create(parent), x =>
            {
                logger.LogInformation("Parent created: {@parent}", x);
                return mapper.Map<ParentDetailsViewModel>(x);
            });
        }

        [HttpPut]
        [Route("{id:long}")]
        [ProducesResponseType(typeof(ParentDetailsViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateParentViewModel updateParentViewModel)
        {
            var parent = mapper.Map<Parent>(updateParentViewModel);
            parent.Id = id;

            return Reply(await parentManager.Update(parent), x =>
            {
                logger.LogInformation("Parent updated: {@parent}", x);
                return mapper.Map<ParentDetailsViewModel>(x);
            });
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
