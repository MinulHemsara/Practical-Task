using Microsoft.AspNetCore.Mvc;
using ptask.Models;
using static ptask.Models.PurchaseRequest;

namespace ptask.Controllers
{
    public class PurchaseController : Controller
    {
        private static List<PurchaseRequest> purchaseRequests = new List<PurchaseRequest>();

        public IActionResult Create()
        {
            return View(new PurchaseRequest());
        }

        [HttpPost]
        public ActionResult Create(PurchaseRequest purchaseRequest)
        {
  
            return View(purchaseRequest);
        }

        public ActionResult Approval()
        {
            var pendingRequests = purchaseRequests.Where(r => r.IsPendingApproval).ToList();
            return View(pendingRequests);
        }

      
        [HttpPost]
        public ActionResult Approve(int id)
        {
            var request = purchaseRequests.FirstOrDefault(r => r.PurchaseRequestNumber == id);
            if (request != null)
            {
                request.IsPendingApproval = false;
            }

            return RedirectToAction("Approval");
        }
        [HttpPost]
        public ActionResult Disapprove(int id)
        {
            UpdateApprovalStatus(id, ApprovalStatus.Disapproved);
            return RedirectToAction("Approval");
        }

        private void UpdateApprovalStatus(int id, ApprovalStatus status)
        {
            var request = purchaseRequests.FirstOrDefault(r => r.PurchaseRequestNumber == id);
            if (request != null)
            {
                request.ApprovalStatus = status;
            }
        }
        public ActionResult Edit(int id)
        {
            var request = purchaseRequests.FirstOrDefault(r => r.PurchaseRequestNumber == id && r.ApprovalStatus == ApprovalStatus.Disapproved);

            if (request == null)
            {
             
                return RedirectToAction("CannotEdit");
            }

            return View(request);
        }

    
        [HttpPost]
        public ActionResult Edit(PurchaseRequest purchaseRequest)
        {

            var existingRequest = purchaseRequests.FirstOrDefault(r => r.PurchaseRequestNumber == purchaseRequest.PurchaseRequestNumber);


            if (existingRequest != null)
            {
                existingRequest.ItemCode = purchaseRequest.ItemCode;
                existingRequest.ItemQuantity = purchaseRequest.ItemQuantity;
                existingRequest.ItemCost = purchaseRequest.ItemCost;

                return RedirectToAction("Approval");
            }

            return RedirectToAction("Error");

        }

    }
}
