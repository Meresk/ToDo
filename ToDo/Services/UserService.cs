using Org.BouncyCastle.Crypto.Generators;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Services
{
    public class UserService(AppDbContext context)
    {
        private readonly AppDbContext _dbContext = context;

        public User_lw5_02? UserVerify(User_lw5_02 user)
        {
            var userExist = _dbContext.Users_lw5_02.FirstOrDefault(u => u.Email == user.Email );

            if (userExist != null && BCrypt.Net.BCrypt.Verify(user.Password, userExist.Password))
            {
                return user;
            }

            return null;
        }
    }
}
