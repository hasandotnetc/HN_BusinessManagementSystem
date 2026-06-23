namespace HN_Frontend.Models
{
    public class EmployeeVM
    {
        public long EmployeeId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; } 
        public string? Picture { get; set; }
    }
}
