using System.Linq;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ToDoManager.Models.Home;
using ToDoManager.Utils;

namespace ToDoManager.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                using ToDoManagerContext context = new ToDoManagerContext();

                User loggedUser = context.Users.FirstOrDefault(u =>
                    u.Username == model.Username &&
                    u.Password == model.Password);

                if (loggedUser == null)
                    ModelState.AddModelError("AuthError", "Invalid username and password!");
                else
                {
                    LoggedUser lu = new LoggedUser
                    {
                        Id = loggedUser.Id,
                        Username = loggedUser.Username,
                        FirstName = loggedUser.FirstName,
                        IsAdmin = loggedUser.IsAdmin,
                        LastName = loggedUser.LastName
                    };
                    HttpContext.Session.SetObjectAsJson("loggedUser", lu);
                }
            }

            if (!ModelState.IsValid)
                return View(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
