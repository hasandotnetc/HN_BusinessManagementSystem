namespace HN_Backend.Helpers
{
    public class CurrentSessionData
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentSessionData(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public long UserId => long.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("userId")!.Value);
        public long CompanyId => long.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("companyId")!.Value);
        public long LocationId => long.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("locationId")!.Value);
        public string JwtIdentifier => _httpContextAccessor.HttpContext!.User.FindFirst("jwtIdentifier")!.Value;
    }
}
