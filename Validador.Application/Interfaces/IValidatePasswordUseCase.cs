namespace Validador.Application.Interfaces;

public interface IValidatePasswordUseCase
{
    void Execute(string password);
}
