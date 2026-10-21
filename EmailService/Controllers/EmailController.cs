using EmailService.DTOs;
using EmailService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace EmailService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailService.Services.EmailService _service;

    public EmailController(EmailService.Services.EmailService service)
    {
        _service = service;
    }

    [HttpPost("boas-vindas")]
    public IActionResult ProcessarBoasVindas([FromBody] UsuarioCriadoEventDto usuario)
    {
        _service.ProcessarBoasVindas(usuario);

        return Ok(new StatusEmailResponseDTo
        {
            Status = "Email processado com sucesso",
            FilaMonitorada = "fila-usuario-criado",
            VerficadoEm = DateTime.UtcNow
        });
    }
}
