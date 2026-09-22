using HN_Backend.Data;
using HN_Backend.DTOs.LoginInformation;
using HN_Backend.Interface;
using System.Security.Cryptography;


namespace HN_Backend.Service
{
    public class UserAuthenticationAndLoginService
    {
        private readonly ISMSorEmailServices _smsOrEmailServices;
        private readonly IUserLoginAndAuthentication _userAuthen;
        private readonly ImageService _imageService;
        private readonly JWTTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventNoOrCodeGeneration _eventNoOrCodeGeneration;
        public UserAuthenticationAndLoginService(IUserLoginAndAuthentication userAuthen, ImageService imgServ, ISMSorEmailServices sMSorEmailServices,JWTTokenService jWTTokenService, IHttpContextAccessor httpContextAccessor,IUnitOfWork unitOfWork,IEventNoOrCodeGeneration eventNoOrCodeGeneration)
        {
            _userAuthen = userAuthen;
            _imageService = imgServ;
            _smsOrEmailServices = sMSorEmailServices;
            _jwtTokenService = jWTTokenService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _eventNoOrCodeGeneration = eventNoOrCodeGeneration;
        }
         

        public async Task<LoginUser> GetUserByUserNameEmailAndPhoneAsync(string paramObj)
        {
            return await _userAuthen.GetUserByUserNameEmailAndPhoneAsync(paramObj);
        }

        public async Task<string> SaveUser(LoginUserEntryVM _loginUser)
        {
            string? imagePath = null;

            try
            {
                if (_loginUser.UserImage != null)
                {
                    imagePath = await _imageService.SaveImageAsync(_loginUser.UserImage, "User");
                }

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(_loginUser.FirstPassword);

                await _unitOfWork.BeginTransactionAsync();
                var userCode = await _eventNoOrCodeGeneration.EventCodeGeneration("User","USR",_loginUser.CompanyId);
                var _user = new LoginUser
                {
                    Name = _loginUser.Name,
                    Code = userCode,
                    Phone = _loginUser.Phone,
                    Email = _loginUser.Email,
                    Address = _loginUser.Address,
                    Picture = imagePath,
                    PasswordHash = passwordHash,
                    LocationId = _loginUser.LocationId,
                    CompanyId = _loginUser.CompanyId,
                    UserLevel = _loginUser.UserLevel
                };
                await _userAuthen.SaveUserAsync(_user);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                if (!string.IsNullOrWhiteSpace(_user.Email))
                {
                    var emailBody = $@"
                                    <html>
                                    <body>
                                        <h2>Welcome to HN Paperless Software</h2>

                                        <p>Hello <strong>{_user.Name}</strong>,</p>

                                         <p>
                                            Your account has been created successfully with the role of <strong>{_user.UserLevel}</strong>.
                                            Your User Code is <strong>{_user.Code}</strong>.
                                        </p>

                                        <p>
                                            Your account is currently pending approval. Please wait for the authorities to review and approve your access.
                                        </p>

                                        <br/>

                                        <p>Regards,</p>
                                        <p><strong>HN Paperless Team</strong></p>
                                    </body>
                                    </html>";

                    try
                    {
                        await _smsOrEmailServices.SendEmailAsync(_user.Email, "Account Created Successfully for HN Paperless Software ", emailBody);
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
                    await _unitOfWork.RollbackTransactionAsync();
                }

                throw;
            }
        }

        public async Task<bool> SendRandomCodeByEmailOrPhone(string objParam)
        {
            string otp= RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
            var user = await _userAuthen.GetUserByUserNameEmailAndPhoneAsync(objParam);
            if (user != null && user.IsActive == true)
            {

                var emailBody = $@"
                                    <html>
                                    <body>
                                        <h2>Password Recovery Code</h2> 
                                        <p>
                                            Dear valuable User, </br> Your password recovery code is: <strong>{otp}</strong>. This code is valid for 5 minutes.
                                        </p>  
                                        <br/> 
                                        <p>Regards,</p>
                                        <p><strong>HN Paperless Team</strong></p>
                                    </body>
                                    </html>";

                UserVerification userVerification = new UserVerification
                {
                    LoginUserId = user.LoginUserId,
                    VerificationCode = otp,
                    ExpiredDate = DateTime.UtcNow.AddMinutes(5),
                    IsUsed = false,
                    VerificationType="Email",
                    IsActive=true
                };
                try
                {
                    await _userAuthen.SaveUserVerificationSendSMS(userVerification);
                    await _unitOfWork.SaveChangesAsync();
                    await _smsOrEmailServices.SendEmailAsync(objParam, "HN Paperless Password Recovery Code", emailBody);
                    return true;
                }
                catch (Exception)
                {

                    throw; 

                }
            }
            return false;
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
             
            var jwtIdentifier = Guid.NewGuid().ToString();             
            var token = _jwtTokenService.GenerateToken(user, jwtIdentifier);             
            var httpContext = _httpContextAccessor.HttpContext;
            var ipAddress = httpContext?.Connection.RemoteIpAddress?.ToString();
            var userAgent = httpContext?.Request.Headers["User-Agent"].ToString();
            var session = new UserSession
            {
                LoginUserId = user.LoginUserId,
                Jwtidentifier = jwtIdentifier,
                CreateOn = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddHours(8),
                IsRevoked = false,
                DeviceName = userAgent,
                IpAddress = ipAddress
            };
            await _userAuthen.SaveUserSessionAsync(session);  
            return new LoginResponseDto
            {
                LoginUserId = user.LoginUserId,
                Code = user.Code,
                Name = user.Name,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<MyProfileDto?> GetMyProfile(long userId)
        {
            var _profileDashboard = await _userAuthen.GetMyProfile(userId);
            if (_profileDashboard == null)
                return null;
             return _profileDashboard;             
        }
        public async Task LogoutAsync(string jwtIdentifier)
        {
            await _userAuthen.UpdateUserSessionForLogoutAsync(jwtIdentifier);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserVerificationBySendCode(string objParam, string OTP)
        {
            var user = await _userAuthen.GetUserByUserNameEmailAndPhoneAsync(objParam);
            var _userVerification = await _userAuthen.CheckValidUserCode(user.LoginUserId, OTP);
            if ((user != null || user.IsActive == true) && _userVerification != null)
            {
                _userVerification.IsVerified = true;
                _userVerification.AttemptCount = 1;
                _userVerification.IsUsed = true;
                _userVerification.IsActive = false;
                _userVerification.VerifiedDate = DateTime.UtcNow; 
                await _userAuthen.UpdateUserVerificationBySendCode(_userVerification);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            return false; 
        }

        public async Task<bool> UpdateLoginUserforResetPassword(string objParam, string password, string confirmPassword)
        {
            var user = await _userAuthen.GetUserByUserNameEmailAndPhoneAsync(objParam); 
            if ((user != null || user.IsActive == true) && password == confirmPassword)
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                user.UpdateOn = DateTime.UtcNow;
                user.PasswordHash = passwordHash; 
                await _userAuthen.UpdateLoginUserforResetPassword(user);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
