namespace Validador.Domain.ValueObject;

public class Password
{
    public string Value { get; }

    public Password(string value)
    {
        Value = value ?? string.Empty;
    }
}
