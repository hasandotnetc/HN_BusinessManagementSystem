using HN_Backend.Data;
using HN_Backend.DTOs.LoginInformation;

namespace HN_Backend.Interface
{
    public interface IUserLoginAndAuthentication
    {
        Task<LoginUser> GetUserByUserNameEmailAndPhoneAsync(string paramObj);
        Task SaveUserAsync(LoginUser _loginUser);
        Task<MyProfileDto?> GetMyProfile(long loginUserId);
        Task SaveUserSessionAsync(UserSession _userSession);
        Task UpdateUserSessionForLogoutAsync(string jwtIdentifier);
        Task SaveUserVerificationSendSMS(UserVerification _varification);
        Task UpdateUserVerificationBySendCode(UserVerification _varification);
        Task<UserVerification?> GetUserVerificationByUserId(long userId);
        Task<UserVerification> CheckValidUserCode(long userId, string otp);
        Task UpdateLoginUserforResetPassword(LoginUser login);
    }
}
