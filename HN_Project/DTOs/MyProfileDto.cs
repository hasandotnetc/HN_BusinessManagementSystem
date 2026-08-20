namespace HN_Backend.DTOs
{
    public class MyProfileDto
    {
        public long LoginUserId { get; set; } 
        public string UserName { get; set; }  
        public string? UserPicture { get; set; } 
        public long LocationId { get; set; }
        public string LocationName { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public string UserLevel { get; set; } 
    }
}
