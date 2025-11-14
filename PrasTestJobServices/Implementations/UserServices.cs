
using Microsoft.EntityFrameworkCore;
using PrasTestJobData;
using PrasTestJobData.Entities;
using PrasTestJobDTO;
using PrasTestJobServices.Abstract;

namespace PrasTestJobServices.Implementations
{
    public class UserServices : IUserServices
    {
        readonly PrasTestJobContext _dbContext;

        public UserServices(PrasTestJobContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateUserAsync(CreateUserDto newUser)
        {
            var userRole = await _dbContext.Roles.SingleOrDefaultAsync(r => r.Name == newUser.RoleName);
            if (userRole is null)
                throw new Exception("Such role does't exists");
            var adedUser = new User { Login = newUser.Login, PasswordHas = newUser.PasswordHas, RoleId = userRole.Id };
            await _dbContext.Users.AddAsync(adedUser);
            await _dbContext.SaveChangesAsync();
            return adedUser.Id;
        }

        public async Task<UserDto?> GetUserByLoginAsync(string login)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Login == login);
            return user is null ? null : new UserDto
            {
                Id = user.Id,
                Login = user.Login,
                PasswordHas = user.PasswordHas,
                RoleId = user.RoleId,
                RoleName = user.Role.Name
            };
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _dbContext.Users.CountAsync();
        }
    }
}
