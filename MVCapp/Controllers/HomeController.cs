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
		public IActionResult Index(string? view, string? order, string? category)
		{
			SidebarViewModel sidebar = GetSidebar(view, order, category);
			IQueryCollection query = HttpContext.Request.Query;

			List<DBModels.Task> tasks = GetTaskList(view, order, category);

			HomePageViewModel context = new HomePageViewModel(query, sidebar, tasks);

			return View("Index", context);
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

		private List<DBModels.Task> GetTaskList(string? view, string? order, string? category)
		{
			List<DBModels.Task> tasks = (from t in _taskRepository.Tasks.ToList()
						join c in _categoryRepository.Categories.ToList()
						on t.CategoryId equals c.Id
						select new DBModels.Task
						{
							Id = t.Id,
							Content = t.Content,
							Deadline = t.Deadline,
							Expired = t.Expired,
							Category = new Category
							{
								Id = c.Id,
								Name = c.Name,
								IconClassList = c.IconClassList
							},
						}).ToList();
			if (category != null) tasks = tasks.Where(t => t.Category.Name == category).ToList();
			switch (view)
			{
				case "current":
					tasks = tasks.Where(t => t.Deadline >= DateTime.Now).ToList();
					break;

				case "expired":
					tasks = tasks.Where(t => t.Deadline < DateTime.Now).ToList();
					break;
			}
			switch (order)
			{
				case "content":
					tasks = tasks.OrderBy(t => t.Content).ToList();
					break;

				case "datetime":
					tasks = tasks.OrderByDescending(t => t.Deadline).ToList();
					break;
				
				default:
					tasks = tasks.OrderByDescending(t => t.Deadline).ToList();
					break;
			}
			return tasks;
		}

		private SidebarViewModel GetSidebar(string? view, string? order, string? category)
		{
			IQueryCollection query = HttpContext.Request.Query;
			Dictionary<string, SidebarSectionItemViewModel> categoriesSectionItems = new Dictionary<string, SidebarSectionItemViewModel>();

			List<Category> categories = _categoryRepository.Categories.ToList();

			categoriesSectionItems.Add("All", SidebarSectionItemViewModel.CategoriesSectionItemAll);
			categoriesSectionItems["All"].ClassName = category ?? "active";

			foreach (Category item in categories)
			{
				categoriesSectionItems.Add(item.Name, new SidebarSectionItemViewModel(item.Name, item.IconClassList));
			}

			SidebarSectionViewModel viewTasksSection = SidebarSectionViewModel.ViewTasksSection();
			SidebarSectionViewModel orderBySection = SidebarSectionViewModel.OrderBySection();
			SidebarSectionViewModel categoriesSection = new SidebarSectionViewModel("Categories", "All", categoriesSectionItems);

			viewTasksSection.Items[view ?? viewTasksSection.SectionItemDefaultName].ClassName = "active";
			orderBySection.Items[order ?? orderBySection.SectionItemDefaultName].ClassName = "active";
			categoriesSection.Items[category ?? categoriesSection.SectionItemDefaultName].ClassName = "active";

			Dictionary<string, SidebarSectionViewModel> sidebarSections = new Dictionary<string, SidebarSectionViewModel>();

			sidebarSections.Add("view", viewTasksSection);
			sidebarSections.Add("order", orderBySection);
			sidebarSections.Add("category", categoriesSection);

			SidebarViewModel sidebar = new SidebarViewModel(sidebarSections);
			sidebar.SetItemsQueryStrings(query);

			return sidebar;
		}
	}
}