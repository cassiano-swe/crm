
using FluentValidation;

namespace crm.api.Entities.Validator;

public class ContactValidator: AbstractValidator<Contact>
{
    public ContactValidator()
    {
        RuleFor(contact => contact.Name).NotEmpty();
        RuleFor(contact => contact.Email).NotEmpty().EmailAddress();
        RuleFor(contact => contact.Mobile).NotEmpty();
        RuleFor(contact => contact.Cpf)
            .Must(IsValidCpf)
            .When(p => !string.IsNullOrWhiteSpace(p.Cpf));
        RuleFor(contact => contact.Cnpj)
            .Must(IsValidCnpj)
            .When(p => !string.IsNullOrWhiteSpace(p.Cnpj));
    }
    
    private static bool IsValidCpf(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return true;

        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
            return false;

        var factor1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var factor2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCpf = cpf.Substring(0, 9);
        var sum = 0;

        for (var i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * factor1[i];

        var remainder = sum % 11;
        var digit1 = remainder < 2 ? 0 : 11 - remainder;

        tempCpf += digit1;
        sum = 0;

        for (int i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * factor2[i];

        remainder = sum % 11;
        var digit2 = remainder < 2 ? 0 : 11 - remainder;

        return cpf.EndsWith($"{digit1}{digit2}");
    }
    
    private bool IsValidCnpj(string? cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return true;

        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpj.Length != 14 || cnpj.All(c => c == cnpj[0]))
            return false;

        var factor1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var factor2 = new[] { 6 }.Concat(factor1).ToArray();

        var temp = cnpj[..12];
        var sum = factor1.Select((f, i) => f * int.Parse(temp[i].ToString())).Sum();

        var remainder = sum % 11;
        var digit1 = remainder < 2 ? 0 : 11 - remainder;

        temp += digit1;
        sum = factor2.Select((f, i) => f * int.Parse(temp[i].ToString())).Sum();

        remainder = sum % 11;
        var digit2 = remainder < 2 ? 0 : 11 - remainder;

        return cnpj.EndsWith($"{digit1}{digit2}");
    }
}