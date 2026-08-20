using HN_Backend.Data;
using HN_Backend.DTOs;
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
        public async Task<MyProfileDto?> GetMyProfile(long loginUserId)
        { 
            var _profile = await (from user in _db.LoginUsers
                           join company in _db.Companies on user.CompanyId equals company.CompanyId
                           join location in _db.Locations on new { user.LocationId, user.CompanyId}  equals new { location.LocationId, location.CompanyId } 
                           where user.LoginUserId == loginUserId
                           select new MyProfileDto
                           {
                               LoginUserId = user.LoginUserId,
                               UserName = user.Name,
                               UserPicture = user.Picture,
                               UserLevel = user.UserLevel,
                               LocationId = location.LocationId,
                               LocationName = location.Name,
                               CompanyId = company.CompanyId,
                               CompanyName = company.Name,
                               CompanyLogo = company.CompanyLogo
                           }).AsNoTracking().FirstOrDefaultAsync();
             return _profile;
        }
        public async Task SaveUserSessionAsync(UserSession _userSession)
        {
            await _db.UserSessions.AddAsync(_userSession);
            await _db.SaveChangesAsync();

        }

        public async Task LogoutAsync(string jwtIdentifier)
        {
            var session = await _db.UserSessions.FirstOrDefaultAsync(x => x.Jwtidentifier == jwtIdentifier && !x.IsRevoked);
            if (session == null)
                return;
            session.IsRevoked = true;
            session.RevokedTime = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }

    }
}
