namespace HN_Backend.DTOs.Supplier
{
    public class SupplierAutocompleteDto
    {
        public long SupplierId { get; set; }  
        public string Name { get; set; }  
        public string Code { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }  // Optional: If you want to include the image URL in the autocomplete results
    }
}
