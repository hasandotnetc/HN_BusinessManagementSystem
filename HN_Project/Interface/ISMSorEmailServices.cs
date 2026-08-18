namespace HN_Backend.Interface
{
    public interface ISMSorEmailServices
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
