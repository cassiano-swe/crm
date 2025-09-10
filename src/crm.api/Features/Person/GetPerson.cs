using crm.api.Database;
using crm.api.Endpoints;
using crm.api.Entities;
using crm.api.Entities.Validator;
using crm.api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace crm.api.Features.Person;

public static class GetPerson
{
    private record Response(
        int Id,
        string Name,
        string? Cpf,
        Organization? Organization,
        string? JobTitle,
        DateTime? BirthDate,
        string? Description,
        Category? Category,
        LeadOrigin? LeadOrigin,
        string? AvailablePhone,
        string? Email,
        string? Facebook,
        string? FaxPhone,
        string? Instagram,
        string? Linkedin,
        string? MobilePhone,
        int? PhoneExtension,
        string? Skype,
        string? Twitter,
        string? Whatsapp,
        string? WorkPhone
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/people/{id}", Handler).WithTags("People");
        }
    }

    private static async Task<IResult> Handler(int id, Context context, HttpContext httpContext)
    {
        var result = await context.People.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

        return result is not null ? Results.Ok(result) : Results.NotFound();
    }
}