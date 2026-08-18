using HN_Backend.Data;
using HN_Backend.Interface;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Repository
{
    public class UserAuthenticationAndLoginRepository: IUserLoginAndAuthentication
    {
        private readonly ApplicationDbContext _db;
        public UserAuthenticationAndLoginRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<LoginUser> GetUserByUserNameEmailAndPhoneAsync(string paramObj)
        {
            return await _db.LoginUsers.FirstOrDefaultAsync(u => (u.Name == paramObj || u.Email == paramObj || u.Phone == paramObj));
        }

        public async Task SaveUserAsync(LoginUser _loginUser)
        {
            await _db.LoginUsers.AddAsync(_loginUser);
            await _db.SaveChangesAsync();
           
        }
    }
}
