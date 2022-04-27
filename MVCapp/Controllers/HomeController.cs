using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCapp.Models;
using MVCapp.ViewModels;
using MVCapp.DBModels;
using Microsoft.Extensions.Primitives;
using System.Dynamic;

namespace MVCapp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ITaskRepository _taskRepository;
		private readonly ICategoryRepository _categoryRepository;

		public HomeController(ILogger<HomeController> logger, ITaskRepository taskRepository, ICategoryRepository categoryRepository)
		{
			_logger = logger;
			_taskRepository = taskRepository;
			_categoryRepository = categoryRepository;
		}

		[HttpGet]
		public IActionResult Index(string? status, string? order)
		{
			SidebarViewModel sidebar = new SidebarViewModel(_categoryRepository, HttpContext.Request.Query);
			IQueryCollection query = HttpContext.Request.Query;

			List<DBModels.Task> tasks = _taskRepository.GetTaskList(status, order, null);

			HomePageViewModel context = new HomePageViewModel(query, sidebar, tasks);

			return View("Index", context);
		}

		[HttpGet]
		[Route("Home/Categories/{categoryName?}/")]
		public IActionResult Categories(string? categoryName, string status, string order)
		{
			IQueryCollection query = HttpContext.Request.Query;
			List<DBModels.Task> tasks = _taskRepository.GetTaskList(status, order, categoryName);
			SidebarViewModel sidebar = new SidebarViewModel(_categoryRepository, query);

			HomePageViewModel viewModel = new HomePageViewModel(query, sidebar, tasks);
			return View("Index", viewModel);
		}

		[HttpGet]
		public IActionResult Create()
		{
			CreateUpdateTaskViewModel viewModel = new CreateUpdateTaskViewModel(_categoryRepository.Categories.ToList());
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Create(DBModels.Task task)
		{
			if (ModelState.IsValid)
			{
				_taskRepository.AddTask(task);
				return RedirectToAction("Index");
			}
			else
			{
				CreateUpdateTaskViewModel viewModel = new CreateUpdateTaskViewModel(_categoryRepository.Categories.ToList());
				return View(viewModel);
			}
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			DBModels.Task? task = _taskRepository.Tasks.FirstOrDefault(t => t.Id == id);
			if (task != null) _taskRepository.RemoveTask(task);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			CreateUpdateTaskViewModel viewModel = new CreateUpdateTaskViewModel(_categoryRepository.Categories.ToList());
			viewModel.Task = _taskRepository.Tasks.FirstOrDefault(t => t.Id == id);
			if (viewModel.Task != null)
				return View(viewModel);
			else
				return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Edit(int id, DBModels.Task task)
		{
			if (ModelState.IsValid)
			{
				task.Id = id;
				_taskRepository.UpdateTask(task);
				return RedirectToAction("Index");
			} else
			{
				CreateUpdateTaskViewModel viewModel = new CreateUpdateTaskViewModel(_categoryRepository.Categories.ToList());
				viewModel.Task = task;
				return View(viewModel);
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}