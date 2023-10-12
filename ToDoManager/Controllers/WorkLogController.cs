using System;
using System.Linq;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoManager.Models.Home;
using ToDoManager.Models.WorkLogs;
using ToDoManager.Utils;
using Task = Data.Entities.Task;

namespace ToDoManager.Controllers
{
    public class WorkLogController : Controller
    {
        // GET: WorkLog
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
            model.Items = context.WorkLogs.Where(i => i.TaskId == model.TaskId).OrderBy(i => i.Id)
                .Skip((model.Page - 1) * model.ItemsPerPage)
                .Take(model.ItemsPerPage)
                .ToList();

            model.PagesCount = (int)Math.Ceiling(
                context.WorkLogs.Count(i => i.TaskId == model.TaskId) / (double)model.ItemsPerPage
            );

            model.Task = context.Tasks.Find(model.TaskId);

            model.UserPairs = context.Users.Select(u => new SelectListItem
            {
                Text = u.Username,
                Value = u.Id.ToString(),
                Selected = u.Id == model.Task.AssigneeId
            }).ToList();

            return View(model);
        }

        // GET: WorkLog/Create
        public ActionResult Create(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            using ToDoManagerContext context = new ToDoManagerContext();
            CreateVM model = new CreateVM
            {
                TaskId = id,
                UserId = HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser").Id
            };

            return View(model);
        }

        // POST: WorkLog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVM model)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            if (!ModelState.IsValid)
                return View(model);

            WorkLog item = new WorkLog
            {
                TaskId = model.TaskId,
                UserId = model.UserId,
                CreateDate = model.Date,
                Hours = model.Hours
            };

            using ToDoManagerContext context = new ToDoManagerContext();
            context.WorkLogs.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index", "WorkLog", new { taskId = model.TaskId });
        }

        // GET: WorkLog/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            using ToDoManagerContext context = new ToDoManagerContext();
            WorkLog item = context.WorkLogs.Find(id);

            EditVM model = new EditVM
            {
                Id = item.Id,
                Hours = item.Hours,
                Date = item.CreateDate,
                TaskId = item.TaskId,
                UserId = item.UserId
            };

            return View(model);
        }

        // POST: WorkLog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditVM model)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            if (!ModelState.IsValid)
                return View(model);

            WorkLog item = new WorkLog
            {
                Id = model.Id,
                TaskId = model.TaskId,
                UserId = model.UserId,
                Hours = model.Hours,
                CreateDate = model.Date
            };

            using ToDoManagerContext context = new ToDoManagerContext();
            context.WorkLogs.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "WorkLog", new { taskId = model.TaskId });
        }

        // GET: WorkLog/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            using ToDoManagerContext context = new ToDoManagerContext();
            WorkLog item = context.WorkLogs.Find(id);
            context.WorkLogs.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index", "WorkLog", new { taskId = item.TaskId });
        }

        [HttpPost]
        public ActionResult TaskEdit(IndexVM model)
        {
            if (HttpContext.Session.GetObjectFromJson<LoggedUser>("loggedUser") == null)
                return RedirectToAction("Login", "Home");

            using ToDoManagerContext context = new ToDoManagerContext();
            Task item = context.Tasks.Find(model.Task.Id);
            item.AssigneeId = model.Task.AssigneeId;
            context.Tasks.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "WorkLog", new { taskId = model.Task.Id });
        }
    }
}