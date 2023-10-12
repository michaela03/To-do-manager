using System;
using System.Linq;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ToDoManager.Models.Home;
using ToDoManager.Models.Projects;
using ToDoManager.Utils;

namespace ToDoManager.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index(IndexVM model)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            model.Page = model.Page <= 0
                ? 1
                : model.Page;

            model.ItemsPerPage = model.ItemsPerPage <= 0
                ? 10
                : model.ItemsPerPage;

            using ToDoManagerContext context = new ToDoManagerContext();
            model.Items = context.Projects.Where(p => p.LeadId == HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser").Id)
                .OrderBy(i => i.Id)
                .Skip((model.Page - 1) * model.ItemsPerPage)
                .Take(model.ItemsPerPage)
                .ToList();

            model.PagesCount = (int)Math.Ceiling(
                context.Users.Count() / (double)model.ItemsPerPage
            );

            return View(model);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVM model)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            if (!ModelState.IsValid)
                return View(model);

            Project item = new Project()
            {
                LeadId = HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser").Id,
                Title = model.Title,
                Description = model.Description
            };

            using ToDoManagerContext context = new ToDoManagerContext();
            context.Projects.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Projet");
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            using ToDoManagerContext context = new ToDoManagerContext();
            Project item = context.Projects.Find(id);

            EditVM model = new EditVM
            {
                Id = item.Id,
                LeadId = item.LeadId.Value,
                Title = item.Title,
                Description = item.Description
            };

            return View(model);
        }

        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditVM model)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            if (!ModelState.IsValid)
                return View(model);

            Project item = new Project
            {
                Id = model.Id,
                LeadId = model.LeadId,
                Title = model.Title,
                Description = model.Description
            };

            using ToDoManagerContext context = new ToDoManagerContext();
            context.Projects.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Project");
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            using ToDoManagerContext context = new ToDoManagerContext();
            context.Projects.Remove(context.Projects.Find(id));
            context.SaveChanges();

            return RedirectToAction("Index", "Project");
        }
    }
}