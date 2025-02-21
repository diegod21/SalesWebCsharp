namespace WebApplication1.Models.ViewModels
{
    public class SalesRecordFormViewModel
    {
        public SalesRecord Sale { get; set; }
        public ICollection<Seller> Sellers { get; set; } 
    }
}
