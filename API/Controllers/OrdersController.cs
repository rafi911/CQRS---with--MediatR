using API.Application.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrdersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return Ok(order);
        }

        [HttpPost("/Update")]
        public async Task<IActionResult> Update(UpdateOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return Ok(order);
        }
    }
}
