using API.ApiResponses;
using API.Application.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<ActionResult<OrderResponse>> PostAsync(CreateOrderCommand command)
        {
            var orderDetail = await _mediator.Send(command);
            return Created("", _mapper.Map<OrderResponse>(orderDetail));
        }

        [HttpPatch("{id}/Update")]
        public async Task<OrderResponse> Confirm([FromRoute] Guid id)
        {
            UpdateOrderCommand command = new(id);
            var orderDetail = await _mediator.Send(command);
            return _mapper.Map<OrderResponse>(orderDetail);
           
        }
    }
}
