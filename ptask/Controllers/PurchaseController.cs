using Microsoft.AspNetCore.Mvc;
using ptask.Data;
using ptask.Models;
using static ptask.Models.PurchaseRequest;

namespace ptask.Controllers
{
    public class PurchaseController : Controller
    {
        private static List<PurchaseRequest> purchaseRequests = new List<PurchaseRequest>();
        private readonly AppDbContext _context;

        public PurchaseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<PurchaseRequest> objPurchaseRequests = _context.PurchaseRequests;
            return View(objPurchaseRequests);
        }

        public IActionResult Create()
        {
            return View(new PurchaseRequest());
        }

        [HttpPost]
        public ActionResult Create(PurchaseRequest purchaseRequest)
        {
            if (ModelState.IsValid)
            {
                _context.PurchaseRequests.Add(purchaseRequest);
                _context.SaveChanges();
                return RedirectToAction("Display", new { id = purchaseRequest.PurchaseRequestNumber });

            }

            return View(purchaseRequest);
        }

        [HttpGet]
        public IActionResult Display(int id)
        {
            var purchaseRequest = _context.PurchaseRequests.Find(id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }

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
