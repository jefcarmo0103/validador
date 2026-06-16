namespace Validador.Domain.Exceptions;

public class PasswordValidationException : Exception
{
    public IReadOnlyList<string> Errors { get; }

    public PasswordValidationException(IEnumerable<string> errors)
        : base("A senha não atende aos critérios de validação.")
    {
        Errors = errors.ToList().AsReadOnly();
    }
}
