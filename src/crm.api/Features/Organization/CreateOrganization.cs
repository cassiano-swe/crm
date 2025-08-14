using crm.api.Database;
using crm.api.Endpoints;
using crm.api.Entities.Validator;
using crm.api.Extensions;

namespace crm.api.features.Organization;

public static class CreateContact
{
    private record Request(
        string Name,
        string? Description,
        string? Cnpj,
        int? CategoryId,
        int? LeadOriginId,
        int? SectorId);

    private record Response(
        int Id,
        string Name,
        string? Description,
        string? Cnpj,
        int? CategoryId,
        int? LeadOriginId,
        int? SectorId
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/organizations", Handler).WithTags("Organizations");
        }
    }

    private static async Task<IResult> Handler(Request request, Context context, HttpContext httpContext)
    {
        var organization = new Entities.Organization(request.Name)
        {
            Description = request.Description,
            Cnpj = request.Cnpj,
            Category = context.Categories.FirstOrDefault(c => c.Id == request.CategoryId),
            LeadOrigin = context.LeadOrigins.FirstOrDefault(l => l.Id == request.LeadOriginId),
            Sector = context.Sectors.FirstOrDefault(s => s.Id == request.SectorId)
        };

        var validator = new OrganizationValidator();
        var validationResult = await validator.ValidateAsync(organization);
        
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        context.Organizations.Add(organization);
        
        await context.SaveChangesAsync();

        return Results.Created($"{httpContext.Request.GetBaseUrl()}/api/organizations/{organization.Id}",
            new Response(Id: organization.Id, Name: organization.Name, Description: organization.Description,
                Cnpj: organization.Cnpj, CategoryId: organization.Category?.Id, organization.LeadOrigin?.Id,
                SectorId: organization.Sector?.Id));
    }
}