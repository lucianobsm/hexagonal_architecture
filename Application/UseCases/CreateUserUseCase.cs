using Application.Ports.Inbound;
using Application.Ports.Outbound.Repository;
using Application.Ports.Outbound.Security;
using Application.UseCases.DTOs;
using Domain.Entity;

namespace Application.UseCases;

public class CreateUserUseCase(IPasswordHasher hasher, IUserRepository repository) : ICreateUserUseCase
{
    public async Task ExecuteAsync(CreateUserDTO user)
    {
        //gerar senha
        var passwordEncripted= hasher.EncriptPassword(user.Password);
        
        //fazer o mapper para dominio
        var domainUser = new User(user.Name, user.Email, passwordEncripted);
        
        //validar campos
        domainUser.ValidateEntity();

        await repository.CreateUserAsync(domainUser);
    }
}