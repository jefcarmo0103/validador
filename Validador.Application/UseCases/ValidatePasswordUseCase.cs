using Validador.Application.Interfaces;
using Validador.Domain.Interfaces;
using Validador.Domain.ValueObject;

namespace Validador.Application.UseCases;

public class ValidatePasswordUseCase : IValidatePasswordUseCase
{
    private readonly IPasswordValidator _passwordValidator;

    public ValidatePasswordUseCase(IPasswordValidator passwordValidator)
    {
        _passwordValidator = passwordValidator;
    }

    public void Execute(string password)
    {
        new Password.Builder(password).Build(_passwordValidator);
    }
}
