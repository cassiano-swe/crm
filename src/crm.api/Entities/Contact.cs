using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace crm.api.Entities;

public record Contact
{ 
    public Guid? Id { get; init; }

    public string Name { get; init; }
    public string Email { get; init; }
    public string Mobile { get; init; }

    public string? Cpf { get; init; }
    public string? Cnpj { get; init; }
    public string? Company { get; init; }
    public string? JobTitle { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? Responsible { get; init; }
    public string? Category { get; init; }
    public string? Source { get; init; }
    public string? Description { get; init; }
    public string? Phone { get; init; }
    public string? Whatsapp { get; init; }
    public string? Extension { get; init; }
    public int? Ranking { get; init; }
    public string? ZipCode { get; init; }
    public string? Country { get; init; }
    public string? State { get; init; }
    public string? City { get; init; }
    public string? District { get; init; }
    public string? Street { get; init; }
    public string? Number { get; init; }
    public string? Complement { get; init; }

    public Contact(string name, string email, string mobile, string? cpf, string? cnpj, string? company,
        string? jobTitle, DateTime? birthDate, string? responsible, string? category, string? source,
        string? description, string? phone, string? whatsapp, string? extension, int? ranking, string? zipCode,
        string? country, string? state, string? city, string? district, string? street, string? number,
        string? complement)
    {
        Name = name;
        Email = email;
        Mobile = mobile;
        Cpf = cpf;
        Cnpj = cnpj;
        Company = company;
        JobTitle = jobTitle;
        BirthDate = birthDate;
        Responsible = responsible;
        Category = category;
        Source = source;
        Description = description;
        Phone = phone;
        Whatsapp = whatsapp;
        Extension = extension;
        Ranking = ranking;
        ZipCode = zipCode;
        Country = country;
        State = state;
        City = city;
        District = district;
        Street = street;
        Number = number;
        Complement = complement;
    }
}
