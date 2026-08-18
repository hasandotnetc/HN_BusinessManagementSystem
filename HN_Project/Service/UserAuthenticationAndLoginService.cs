using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.Interface; 


namespace HN_Backend.Service
{
    public class UserAuthenticationAndLoginService
    {
        private readonly ISMSorEmailServices _smsOrEmailServices;
        private readonly IUserLoginAndAuthentication _userAuthen;
        private readonly ImageService _imageService;
        private readonly JWTTokenService _jwtTokenService;
        public UserAuthenticationAndLoginService(IUserLoginAndAuthentication userAuthen, ImageService imgServ, ISMSorEmailServices sMSorEmailServices,JWTTokenService jWTTokenService)
        {
            _userAuthen = userAuthen;
            _imageService = imgServ;
            _smsOrEmailServices = sMSorEmailServices;
            _jwtTokenService = jWTTokenService;
        }
         

        public async Task<LoginUser> GetUserByUserNameEmailAndPhoneAsync(string paramObj)
        {
            return await _userAuthen.GetUserByUserNameEmailAndPhoneAsync(paramObj);
        }

        public async Task<string> SaveUser(LoginUserEntryVM vm)
        {
            string? imagePath = null;

            try
            {
                if (vm.UserImage != null)
                {
                    imagePath = await _imageService.SaveImageAsync(vm.UserImage, "User");
                }

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(vm.FirstPassword);

                var _user = new LoginUser
                {
                    Name = vm.Name,
                    Code =  "SUP-0010",
                    Phone = vm.Phone,
                    Email = vm.Email,
                    Address = vm.Address,
                    Picture = imagePath,
                    PasswordHash = passwordHash,
                    LocationId = 1,
                    UserLevel = "Admin"
                };

                await _userAuthen.SaveUserAsync(_user); 
                 
                if (!string.IsNullOrWhiteSpace(_user.Email))
                {
                    var emailBody = $@"
                                    <html>
                                    <body>
                                        <h2>Welcome to HN ERP</h2>

                                        <p>Hello <strong>{_user.Name}</strong>,</p>

                                        <p>
                                            Your account has been created successfully.
                                        </p>

                                        <p>
                                            You can now use your account to access the system.
                                        </p>

                                        <br/>

                                        <p>Regards,</p>
                                        <p><strong>HN ERP Team</strong></p>
                                    </body>
                                    </html>";

                    try
                    {
                        await _smsOrEmailServices.SendEmailAsync(_user.Email,"HN Paperless Software Account Created Successfully",emailBody);
                    }
                    catch (Exception ex)
                    {
                        // Email failed, but user was already created.
                        // Log the exception here.
                    }

                }

                    return _user.Code;
            }
            catch
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    await _imageService.DeleteImageAsync(imagePath);
                }

                throw;
            }
        }


        public async Task<LoginResponseDto?> LoginUserAsync(string paramObj, string password)
        {
            var user = await _userAuthen.GetUserByUserNameEmailAndPhoneAsync(paramObj);

            if (user == null)
                return null;

            if (!user.IsActive)
                return null;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isPasswordValid)
                return null;

            // JWT Generate
            var token = _jwtTokenService.GenerateToken(user);
            return new LoginResponseDto
            {
                LoginUserId = user.LoginUserId,
                Code = user.Code,
                Name = user.Name,
                Email = user.Email,
                Token = token
            };
        }

    }
}
