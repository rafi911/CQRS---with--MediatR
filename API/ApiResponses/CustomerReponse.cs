using System;

namespace API.ApiResponses
{
    public record CustomerReponse(string FirstName, string LastName, DateTimeOffset DateOfBirth)
    {
    }
}
