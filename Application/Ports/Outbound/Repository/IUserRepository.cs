using Domain.Entity;

namespace Application.Ports.Outbound.Repository;

public interface IUserRepository
{
    Task CreateUserAsync(User user);
}