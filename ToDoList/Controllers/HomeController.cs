// using System.Collections.Generic;
// using System;
// using Microsoft.AspNetCore.Mvc;
// using ToDoList.Models;
//
// namespace TodoList.Controllers
// {
//   public class HomeController : Controller
//   {
//     [HttpGet("/")]
//     public ActionResult Index()
//     {
//       List<Task> categoryTasks = Task.GetAll();
//
//       return View(categoryTasks);
//     }
//
//     [HttpGet("/category/add")]
//     public ActionResult AddCategory()
//     {
//       return View();
//     }
//
//     [HttpPost("/category/list")]
//     public ActionResult WriteCategories()
//     {
//       Category newCategory = new Category(Request.Form["category-name"]);
//       newCategory.Save();
//       List<Category> allCategories = Category.GetAll();
//
//       return View("ViewCategories", allCategories);
//     }
//
//     [HttpGet("/category/list")]
//     public ActionResult ReadCategories()
//     {
//       List<Category> allCategories = Category.GetAll();
//
//       return View("ViewCategories", allCategories);
//     }
//
//     [HttpGet("/{name}/{id}/tasklist")]
//     public ActionResult ViewTaskList(int id)
//     {
//       Console.WriteLine("hello we are in Get");
//
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Category selectedCategory = Category.Find(id); //Category is selected as an object
//       List<Task> categoryTasks = selectedCategory.GetTasks(); //Tasks are displayed in a list
//
//       model.Add("category", selectedCategory);
//       model.Add("tasks", categoryTasks);
//
//       //return the task list for selected category
//       return View("CategoryDetail", model);
//     }
//
//     [HttpGet("/{name}/{id}/task/add")]
//     public ActionResult AddTask(int id)
//     {
//       Category selectedCategory = Category.Find(id); //Category is selected as an object
//
//       return View(selectedCategory);
//
//     }
//
//     [HttpPost("/{name}/{id}/tasklist")]
//     public ActionResult AddTaskViewTaskList(int id)
//     {
//       Task newTask = new Task(Request.Form["task-name"], id, Request.Form["task-dueDate"]);
//       newTask.Save();
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Category selectedCategory = Category.Find(id); //Category is selected as an object
//       List<Task> categoryTasks = selectedCategory.GetTasks(); //Tasks are displayed in a list
//       Console.WriteLine(id);
//
//       model.Add("category", selectedCategory);
//       model.Add("tasks", categoryTasks);
//       Console.WriteLine(categoryTasks[0].GetDescription());
//
//       //return the task list for selected category
//       return View("CategoryDetail", model);
//     }
//
//     [HttpGet("/tasks/{id}/edit")]
//     public ActionResult TaskEdit(int id)
//     {
//       // Dictionary<string, object> model = new Dictionary<string, object>();
//       // Category selectedCategory = Category.Find(id); //Category is selected as an object
//       // List<Task> categoryTasks = selectedCategory.GetTasks(); //Tasks are displayed in a list
//       //
//       // model.Add("category", selectedCategory);
//       // model.Add("tasks", categoryTasks);
//
//       Task thisTask = Task.Find(id);
//       return View(thisTask);
//     }
//
//     [HttpPost("/tasks/{id}/edit")]
//     public ActionResult TaskEditConfirm(int id)
//     {
//       Task thisTask = Task.Find(id);
//       thisTask.UpdateDescription(Request.Form["new-name"]);
//       return RedirectToAction("Index");
//     }
//     [HttpGet("/tasks/{id}/delete")]
//     public ActionResult TaskDeleteConfirm(int id)
//     {
//       Task thisTask = Task.Find(id);
//       thisTask.DeleteTask();
//       return RedirectToAction("Index");
//     }
//
//
//   }
// }
