using System.ComponentModel.DataAnnotations;

namespace ptask.Models
{
    public enum ApprovalStatus
    {
        Pending,
        Approved,
        Disapproved
    }
    public class PurchaseRequest
    {


        [Key]
        public int PurchaseRequestNumber { get; set; }
        public string ItemCode { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemCost { get; set; }
        public bool IsPendingApproval { get; set; } = true;
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;


        public decimal TotalCost => ItemQuantity * ItemCost;
    }
}
