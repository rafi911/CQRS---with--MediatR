using System;

namespace API.ApiResponses
{
    public record CustomerReponse(Guid Id, string FirstName, string LastName, DateTimeOffset DateOfBirth)
    {
    }
}
