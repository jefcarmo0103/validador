using Validador.Domain.ValueObject;

namespace Validador.Domain.Interfaces;

public interface IPasswordValidator
{
    void Validate(Password password);
}
