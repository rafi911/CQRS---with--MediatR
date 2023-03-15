using Domain.Aggregates.CustomerAggregate;
using MediatR;
using System;

namespace API.Application.Commands
{
    public record CreateCustomerCommand(string FirstName, string LastName, DateTimeOffset DateOfBirth) : IRequest<Customer> { }
}
