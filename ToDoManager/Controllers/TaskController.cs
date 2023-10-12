using System;
using System.Linq;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoManager.Models.Home;
using ToDoManager.Models.Tasks;
using ToDoManager.Utils;

namespace ToDoManager.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
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
            model.Items = context.Tasks.Where(i => i.ProjectId == model.ProjectId).OrderBy(i => i.Id)
                .Skip((model.Page - 1) * model.ItemsPerPage)
                .Take(model.ItemsPerPage)
                .ToList();

            model.PagesCount = (int)Math.Ceiling(
                context.Tasks.Count(i => i.ProjectId == model.ProjectId) / (double)model.ItemsPerPage
            );

            model.Project = context.Projects.Find(model.ProjectId);

            return View(model);
        }

        // GET: Task/Create
        public ActionResult Create(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            using ToDoManagerContext context = new ToDoManagerContext();
            CreateVM model = new CreateVM
            {
                ProjectId = id,
                UserPairs = context.Users.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Username }).ToList()
            };

            return View(model);
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVM model)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            if (!ModelState.IsValid)
                return View(model);

            Task item = new Task
            {
                ProjectId = model.ProjectId,
                Title = model.Title,
                Description = model.Description,
                AssigneeId = model.AssigneeId
            };

            using ToDoManagerContext context = new ToDoManagerContext();
            context.Tasks.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Task", new { projectId = model.ProjectId });
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            using ToDoManagerContext context = new ToDoManagerContext();
            Task item = context.Tasks.Find(id);

            EditVM model = new EditVM
            {
                Id = item.Id,
                ProjectId = item.ProjectId,
                Title = item.Title,
                Description = item.Description,
                UserPairs = context.Users.Select(u => new SelectListItem
                {
                    Text = u.Username,
                    Value = u.Id.ToString(),
                    Selected = item.AssigneeId == u.Id
                }).ToList()
            };

            return View(model);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditVM model)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            if (!ModelState.IsValid)
                return View(model);

            Task item = new Task
            {
                Id = model.Id,
                ProjectId = model.ProjectId,
                Title = model.Title,
                Description = model.Description,
                AssigneeId = model.AssigneeId
            };

            using ToDoManagerContext context = new ToDoManagerContext();
            context.Tasks.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Task", new { projectId = model.ProjectId });
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            using ToDoManagerContext context = new ToDoManagerContext();
            Task item = context.Tasks.Find(id);
            context.Tasks.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Task", new { projectId = item.ProjectId });
        }

        [HttpGet]
        public ActionResult MyTasks(MyTasksVM model)
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
            model.Items = context.Tasks.Where(i => i.AssigneeId == HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser").Id)
                .OrderBy(i => i.Id)
                .Skip((model.Page - 1) * model.ItemsPerPage)
                .Take(model.ItemsPerPage)
                .Select(t => new MyTasksVM.MyTasks
                {
                    TaskId = t.Id,
                    ProjectTitle = t.Project.Title,
                    TaskTitle = t.Title
                })
                .ToList();

            model.PagesCount = (int)Math.Ceiling(
                context.Tasks.Count(i => i.AssigneeId == HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser").Id) / (double)model.ItemsPerPage
            );

            return View(model);
        }
    }
}