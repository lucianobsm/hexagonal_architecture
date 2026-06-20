using Application.UseCases.DTOs;

namespace Application.Ports.Inbound;

public interface ICreateUserUseCase
{
    Task ExecuteAsync(CreateUserDTO user);
}