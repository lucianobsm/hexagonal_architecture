using Application.Ports.Outbound.Repository;
using Domain.Entity;
using Infrastructure.Adapters.Outbound.Database;
using Infrastructure.Adapters.Outbound.Database.Entity;

namespace Infrastructure.Adapters.Outbound.Repository;

public class UserRepository(MyDatabase context) : IUserRepository
{
    public async Task CreateUserAsync(User user)
    {
        var entity = new UserEntity();

        context.Users.Add(entity);
        await context.SaveChangesAsync();
    }
}