using System.ComponentModel.DataAnnotations;
using crm.api.Database;
using crm.api.Endpoints;
using crm.api.Entities;
using crm.api.Entities.Validator;
using crm.api.Extensions;
using FluentValidation.Results;

namespace crm.api.features.Contacts;

public static class CreateContact
{
    private record Request(
        string Name,
        string Email,
        string Mobile,
        string? Cpf = null,
        string? Cnpj = null,
        string? Company = null,
        string? JobTitle = null,
        DateTime? BirthDate = null,
        string? Responsible = null,
        string? Category = null,
        string? Source = null,
        string? Description = null,
        string? Phone = null,
        string? Whatsapp = null,
        string? Extension = null,
        int? Ranking = null,
        string? ZipCode = null,
        string? Country = null,
        string? State = null,
        string? City = null,
        string? District = null,
        string? Street = null,
        string? Number = null,
        string? Complement = null
    );

    private record Response(
        Guid Id,
        string Name,
        string Email,
        string Mobile,
        string? Cpf = null,
        string? Cnpj = null,
        string? Company = null,
        string? JobTitle = null,
        DateTime? BirthDate = null,
        string? Responsible = null,
        string? Category = null,
        string? Source = null,
        string? Description = null,
        string? Phone = null,
        string? Whatsapp = null,
        string? Extension = null,
        int? Ranking = null,
        string? ZipCode = null,
        string? Country = null,
        string? State = null,
        string? City = null,
        string? District = null,
        string? Street = null,
        string? Number = null,
        string? Complement = null
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/contacts", Handler).WithTags("Contacts");
        }
    }

    private static async Task<IResult> Handler(Request request, Context context, HttpContext httpContext)
    {
        var contact = new Contact(
            request.Name,
            request.Email,
            request.Mobile,
            request.Cpf,
            request.Cnpj,
            request.Company,
            request.JobTitle,
            request.BirthDate,
            request.Responsible,
            request.Category,
            request.Source,
            request.Description,
            request.Phone,
            request.Whatsapp,
            request.Extension,
            request.Ranking,
            request.ZipCode,
            request.Country,
            request.State,
            request.City,
            request.District,
            request.Street,
            request.Number,
            request.Complement
        );

        var validator = new ContactValidator();
        var validationResult = await validator.ValidateAsync(contact);
        
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        context.Contacts.Add(contact);
        
        await context.SaveChangesAsync();

        return Results.Created($"{httpContext.Request.GetBaseUrl()}/api/contacts/{contact.Id}",
            new Response(Id: (Guid)contact.Id!, Name: contact.Name, Email: contact.Email, Mobile: contact.Mobile));
    }
}