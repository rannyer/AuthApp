namespace Shared.DTOs;

public class UsuarioCriadoEventDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DataCriacao { get; set; }
}