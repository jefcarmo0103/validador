using Validador.Domain.Exceptions;
using Validador.Domain.Interfaces;
using Validador.Domain.Services;
using Validador.Domain.ValueObject;

namespace Validador.UnitTest;

public class PasswordValidatorTests
{
    private readonly IPasswordValidator _validator = new PasswordValidator();

    private bool IsValid(string password)
    {
        try
        {
            new Password.Builder(password).Build(_validator);
            return true;
        }
        catch (PasswordValidationException)
        {
            return false;
        }
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("aa", false)]
    [InlineData("ab", false)]
    [InlineData("AAAbbbCc", false)]
    [InlineData("AbTp9!foo", false)]
    [InlineData("AbTp9!foA", false)]
    [InlineData("AbTp9 fok", false)]
    [InlineData("AbTp9!fok", true)]
    public void Validate(string password, bool expected)
    {
        Assert.Equal(expected, IsValid(password));
    }
}
