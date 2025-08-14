using FluentValidation;

namespace crm.api.Entities.Validator;

public class OrganizationValidator:AbstractValidator<Organization>
{
    public OrganizationValidator()
    {
        RuleFor(contact => contact.Name).NotEmpty();
        RuleFor(contact => contact.Cnpj)
            .Must(IsValidCnpj)
            .When(p => !string.IsNullOrWhiteSpace(p.Cnpj));
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