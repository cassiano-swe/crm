using crm.api.Database;
using crm.api.Endpoints;
using crm.api.Entities;
using crm.api.Entities.Validator;
using crm.api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace crm.api.Features.Person;

public static class DeletePerson
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/people", Handler).WithTags("People");
        }
    }

    private static async Task<IResult> Handler(int id, Context context, HttpContext httpContext)
    {
        Entities.Person? result = await context.People.FindAsync(id);

        if (result is null)
            return Results.NotFound();
        else
        {
            context.People.Remove(result);
            context.SaveChanges();
            return Results.NoContent();
        }
    }
}