namespace HN_Project.DTOs
{
    public class ProductVM
    {
        public long ProductId { get; set; }

        public long CategoryId { get; set; }

        public long BrandId { get; set; }
        public long GroupId { get; set; }

        public string? Code { get; set; } 

        public string Name { get; set; } = null!;

        public string? Model { get; set; }

        public string SerialAvailable { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal Vat { get; set; }

        public decimal Tax { get; set; }

        public decimal Warranty { get; set; }

        public string? Picture { get; set; }

        //public DateTime? CreateOn { get; set; }
        public IFormFile? ProductImage { get; set; }

        public long EntryBy { get; set; } = 1;

    }
}
