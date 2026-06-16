namespace Validador.Domain.ValueObject;

public sealed class Password
{
    public string Value { get; }

    private Password(string value)
    {
        Value = value;
    }

    public sealed class Builder(string value)
    {
        private readonly string _value = value ?? string.Empty;

        public Password Build(Interfaces.IPasswordValidator validator)
        {
            var password = new Password(_value);
            validator.Validate(password);
            return password;
        }
    }
}
