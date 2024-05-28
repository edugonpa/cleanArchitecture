using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Infrastructure;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Email recipient, string subject, string body)
    {
        //Simular envió de correo
        return Task.CompletedTask;
    }
}