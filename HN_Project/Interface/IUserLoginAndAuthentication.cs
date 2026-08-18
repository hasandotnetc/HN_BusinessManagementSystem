using HN_Backend.Data;

namespace HN_Backend.Interface
{
    public interface IUserLoginAndAuthentication
    {
        Task<LoginUser>  GetUserByUserNameEmailAndPhoneAsync(string paramObj);
        Task SaveUserAsync(LoginUser _loginUser);

    }
}
