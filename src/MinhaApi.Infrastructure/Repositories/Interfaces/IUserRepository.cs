using MinhaApi.Entities;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(Guid id);
    Task AddUserAsync(User user);

    // Novos métodos
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(User user);
}
