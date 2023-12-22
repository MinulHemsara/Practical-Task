using System.ComponentModel.DataAnnotations;

namespace ptask.Models
{
  
    public class PurchaseRequest
    {


        [Key]
        public int PurchaseRequestNumber { get; set; }
        public string ItemCode { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemCost { get; set; }
        public bool IsApproved { get; set; }


        public decimal TotalCost => ItemQuantity * ItemCost;
    }
}
