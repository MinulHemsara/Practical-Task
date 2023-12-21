using Microsoft.AspNetCore.Mvc;
using ptask.Models;
using System.Web.Mvc;


namespace ptask.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLoginView model)
        {
            if (IsValidUser(model))
            {
                
                Session["User"] = model.Username;

                return RedirectToAction("Index", "Purchase");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        private bool IsValidUser(UserLoginView model)
        {
          
            return model.Username == "user" && model.Password == "userpassword";
        }
    }
}
