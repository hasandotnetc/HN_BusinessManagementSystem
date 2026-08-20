using HN_Backend.Data;
using HN_Backend.DTOs;

namespace HN_Backend.Interface
{
    public interface IUserLoginAndAuthentication
    {
        Task<LoginUser>  GetUserByUserNameEmailAndPhoneAsync(string paramObj);
        Task SaveUserAsync(LoginUser _loginUser);
        Task<MyProfileDto?> GetMyProfile(long loginUserId);
        Task SaveUserSessionAsync(UserSession _userSession);
        Task LogoutAsync(string jwtIdentifier);
    }
}
