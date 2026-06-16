using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Validador.Application.Interfaces;

namespace Validador.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PasswordController : ControllerBase
{
    private readonly IValidatePasswordUseCase _validatePasswordUseCase;

    public PasswordController(IValidatePasswordUseCase validatePasswordUseCase)
    {
        _validatePasswordUseCase = validatePasswordUseCase;
    }

    [HttpPost("validate")]
    public IActionResult Validate([FromBody] PasswordRequest request)
    {
        _validatePasswordUseCase.Execute(request.Password);
        return Ok(new { isValid = true });
    }
}

public record PasswordRequest([Required(AllowEmptyStrings = false)] string Password);
