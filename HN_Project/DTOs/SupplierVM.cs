namespace HN_Backend.DTOs
{
    public class SupplierVM
    {
        public long SupplierId { get; set; }

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Picture { get; set; }
        public IFormFile? SupplierImage { get; set; }

        //public DateTime CreateOn { get; set; }

        //public long EntryBy { get; set; } 

    }
}
