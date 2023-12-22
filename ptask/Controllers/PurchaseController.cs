using Microsoft.AspNetCore.Mvc;
using ptask.Data;
using ptask.Models;
using static ptask.Models.PurchaseRequest;

namespace ptask.Controllers
{
    public class PurchaseController : Controller
    {
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

        public IActionResult PendingRequests()
        {
            var pendingRequests = _context.PurchaseRequests.Where(p => !p.IsApproved).ToList();
            return View(pendingRequests);
        }

        [HttpPost]
        public IActionResult Approve(int purchaseRequestId, string approval)
        {
            var request = _context.PurchaseRequests.Find(purchaseRequestId);

            if (request != null)
            {
                if (approval.ToLower() == "approve")
                {
                    request.IsApproved = true;

                }
                else if (approval.ToLower() == "disapprove")
                {

                }

                _context.SaveChanges();
            }

            return RedirectToAction("PendingRequests");
        }

    }
}
