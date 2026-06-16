using Validador.API.Middleware;
using Validador.Application.Interfaces;
using Validador.Application.UseCases;
using Validador.Domain.Interfaces;
using Validador.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPasswordValidator, PasswordValidator>();
builder.Services.AddScoped<IValidatePasswordUseCase, ValidatePasswordUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
