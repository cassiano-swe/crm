using crm.api.Database;
using crm.api.Endpoints;
using crm.api.Entities;
using crm.api.Entities.Validator;
using crm.api.Extensions;

namespace crm.api.Features.Person;

public static class UpdatePerson
{
    internal record Request(
        int Id,
        string Name,
        string? Cpf,
        int? Organization,
        string? JobTitle,
        DateTime? BirthDate,
        string? Description,
        int? CategoryId,
        int? LeadOriginId,
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
        string? WorkPhone);

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
            app.MapPut("api/people", Handler).WithTags("People");
        }
    }

    private static async Task<IResult> Handler(Request request, Context context, HttpContext httpContext)
    {
        var personForUpdate = Entities.Person.CreatePersonWithContact(
            Id: request.Id,
            name: request.Name,
            cpf: request.Cpf,
            organization: request.Organization.HasValue
                ? await context.Organizations.FindAsync(request.Organization.Value)
                : null,
            jobTitle: request.JobTitle,
            birthDate: request.BirthDate,
            description: request.Description,
            category: request.CategoryId.HasValue
                ? await context.Categories.FindAsync(request.CategoryId.Value)
                : null,
            leadOrigin: request.LeadOriginId.HasValue
                ? await context.LeadOrigins.FindAsync(request.LeadOriginId.Value)
                : null,
            availablePhone: request.AvailablePhone,
            email: request.Email,
            facebook: request.Facebook,
            faxPhone: request.FaxPhone,
            instagram: request.Instagram,
            linkedin: request.Linkedin,
            mobilePhone: request.MobilePhone,
            phoneExtension: request.PhoneExtension,
            skype: request.Skype,
            twitter: request.Twitter,
            whatsapp: request.Whatsapp,
            workPhone: request.WorkPhone);

        var validator = new PersonValidator();
        var validationResult = await validator.ValidateAsync(personForUpdate);

        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        var person = await context.People.FindAsync(personForUpdate.Id);

        if (person is null)
        {
            return Results.NotFound();
        }

        context.Entry(person).CurrentValues.SetValues(personForUpdate);

        person.CreateContact(availablePhone: personForUpdate.Contact?.AvailablePhone,
                    email: personForUpdate.Contact?.Email,
                    facebook: personForUpdate.Contact?.Facebook,
                    faxPhone: personForUpdate.Contact?.FaxPhone,
                    instagram: personForUpdate.Contact?.Instagram,
                    linkedin: personForUpdate.Contact?.Linkedin,
                    mobilePhone: personForUpdate.Contact?.MobilePhone,
                    phoneExtension: personForUpdate.Contact?.PhoneExtension,
                    skype: personForUpdate.Contact?.Skype,
                    twitter: personForUpdate.Contact?.Twitter,
                    whatsapp: personForUpdate.Contact?.Whatsapp,
                    workPhone: personForUpdate.Contact?.WorkPhone);

        await context.SaveChangesAsync();

        return Results.Ok(
            new Response(
                Id: person.Id,
                Name: person.Name,
                Cpf: person.Cpf,
                Organization: person.Organization,
                JobTitle: person.JobTitle,
                BirthDate: person.BirthDate,
                Description: person.Description,
                Category: person.Category,
                LeadOrigin: person.LeadOrigin,
                AvailablePhone: person.Contact?.AvailablePhone,
                Email: person.Contact?.Email,
                Facebook: person.Contact?.Facebook,
                FaxPhone: person.Contact?.FaxPhone,
                Instagram: person.Contact?.Instagram,
                Linkedin: person.Contact?.Linkedin,
                MobilePhone: person.Contact?.MobilePhone,
                PhoneExtension: person.Contact?.PhoneExtension,
                Skype: person.Contact?.Skype,
                Twitter: person.Contact?.Twitter,
                Whatsapp: person.Contact?.Whatsapp,
                WorkPhone: person.Contact?.WorkPhone));
    }
}