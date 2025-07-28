using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApp.Controllers
{
    public class TodoController : Controller
    {
        // 仮のデータストア
        private static List<Todo> Todos = new List<Todo>();

        public IActionResult Index()
        {
            return View(Todos);
        }

        [HttpPost]
        public IActionResult Add(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                Todos.Add(new Todo { Id = Todos.Count + 1, Title = title, IsCompleted = false });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Complete(int id)
        {
            var todo = Todos.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                todo.IsCompleted = true;
            }
            return RedirectToAction("Index");
        }
    }
}