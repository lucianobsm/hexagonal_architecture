using Application.Ports.Inbound;
using Application.UseCases.DTOs;

namespace API.Endpoint;

public static class User
{
    public static void RegisterUserEndpoints(this WebApplication app)
    {
        var user = app.MapGroup("/user");

        user.MapPost("/", CreateUser);
    }

    private static Task CreateUser(CreateUserDTO user, ICreateUserUseCase  createUser)
    {
        return createUser.ExecuteAsync(user);
    }
}
