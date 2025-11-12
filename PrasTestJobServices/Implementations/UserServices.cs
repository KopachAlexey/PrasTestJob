
using PrasTestJobDTO;
using PrasTestJobServices.Abstract;

namespace PrasTestJobServices.Implementations
{
    public class UserServices : IUserServices
    {
        public Task<Guid> CreateUserAsync(CreateUserDto newUser)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUserByLoginAsync(string login)
        {
            throw new NotImplementedException();
        }
    }
}
