using PrasTestJobDTO;

namespace PrasTestJobServices.Abstract
{
    public interface IUserServices
    {
        Task<Guid> CreateUserAsync(CreateUserDto newUser);
        Task<UserDto> GetUserByLoginAsync(string login);
    }
}
