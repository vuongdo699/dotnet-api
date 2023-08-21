using ApplicationCore.Command.Features.Chemicals.Commands;
using ApplicationCore.Query.Chemicals;
using ApplicationCore.Query.Chemicals.Models;
using ApplicationCore.Query.Seedwork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("chemicals")]
    public class ChemicalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChemicalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ChemicalVm>>> GetChemicals(
            [FromQuery] QueryOptions option)
        {
            var query = new GetChemicalsQuery() { QueryOptions = option };
            var result = await _mediator.Send(query);
            return result;
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromBody] AddChemicalCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpGet("dropdown/type")]
        public async Task<ActionResult<List<DropdownItem<int>>>> GetChemicalTypeDropdown(string keyword)
        {
            var query = new GetChemicalTypeDropdownQuery() { Keyword = keyword };
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
