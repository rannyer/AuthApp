using Shared.DTOs;

namespace EmailService.Services;

public class EmailService
{
    public void ProcessarBoasVindas(UsuarioCriadoEventDto usuario)
    {
        Console.WriteLine("Enviei o email");
    }
}