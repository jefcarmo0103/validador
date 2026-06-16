using FluentValidation;
using Validador.Domain.Exceptions;
using Validador.Domain.Interfaces;
using Validador.Domain.ValueObject;

namespace Validador.Domain.Services;

public class PasswordValidator : AbstractValidator<Password>, IPasswordValidator
{
    private const string SpecialChars = "!@#$%^&*()-+";

    public PasswordValidator()
    {
        RuleFor(p => p.Value)
            .MinimumLength(9).WithMessage("Deve conter ao menos 9 caracteres.");

        RuleFor(p => p.Value)
            .Matches(@"\d").WithMessage("Deve conter ao menos 1 dígito.");

        RuleFor(p => p.Value)
            .Matches(@"[a-z]").WithMessage("Deve conter ao menos 1 letra minúscula.");

        RuleFor(p => p.Value)
            .Matches(@"[A-Z]").WithMessage("Deve conter ao menos 1 letra maiúscula.");

        RuleFor(p => p.Value)
            .Must(v => v.Any(c => SpecialChars.Contains(c)))
            .WithMessage("Deve conter ao menos 1 caractere especial (!@#$%^&*()-+).");

        RuleFor(p => p.Value)
            .Must(v => v.Distinct().Count() == v.Length)
            .WithMessage("Não deve possuir caracteres repetidos.");
    }

    void IPasswordValidator.Validate(Password password)
    {
        var result = this.Validate(password);

        if (!result.IsValid)
            throw new PasswordValidationException(result.Errors.Select(e => e.ErrorMessage));
    }
}
