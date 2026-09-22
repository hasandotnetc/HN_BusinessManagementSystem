namespace HN_Backend.DTOs.Employee
{
    public class EmployeeVM
    {
        public long EmployeeId { get; set; } 
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        //public string? Nid { get; set; }
        public string? Picture { get; set; }
    }
}
