using System;

namespace HN_Project.DTOs
{ 
    public class CurrentStockProductVM
    {
        public long ProductId { get; set; } 
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Model { get; set; }

        //public string SerialAvailable { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        //public decimal Vat { get; set; }

        //public decimal Tax { get; set; }

        public decimal Warranty { get; set; }

        public string? Picture { get; set; } 
        public double Quantity { get; set; }

        public decimal Cost { get; set; } 
        public string SerialNo { get; set; } = null!;
    }
}
