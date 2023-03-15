using API.ApiResponses;
using API.Application.Commands;
using API.Application.Queries;
using AutoMapper;
using Domain.Aggregates.CustomerAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CustomersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateCustomerCommand command)
        {
            var customer = await _mediator.Send(command);
            return Ok(_mapper.Map<CustomerReponse>(customer));
        }

        [HttpGet("{id}")]
        public async Task<CustomerReponse> Get([FromRoute] Guid id)
        {
            GetCustomerQuery customerQuery = new(id);
            var customer = await _mediator.Send(customerQuery);

            return _mapper.Map<CustomerReponse>(customer);
        }

        [HttpGet]
        public async Task<List<CustomerReponse>> Get()
        {
            GetCustomersQuery customerQuery = new();
            var customers = await _mediator.Send(customerQuery);

            return _mapper.Map<List<CustomerReponse>>(customers);
        }
    }
}
