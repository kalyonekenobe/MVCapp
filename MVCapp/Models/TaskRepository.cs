using MVCapp.DBModels;

namespace MVCapp.Models
{
	public class TaskRepository : ITaskRepository
	{
		private readonly ApplicationDBContext _context;

		public TaskRepository(ApplicationDBContext context)
		{
			_context = context;
			foreach (DBModels.Task task in _context.Tasks)
			{
				if (task.Deadline < DateTime.Now)
				{
					task.Expired = true;
				}
			}
			_context.SaveChanges();
		}

		public IEnumerable<DBModels.Task> Tasks => _context.Tasks;

		public void AddTask(DBModels.Task task)
		{
			_context.Tasks.Add(task);
			_context.SaveChanges(true);
		}

		public List<DBModels.Task> GetTaskList(string? status, string? order, string? categoryName)
		{
			List<Category> categories = _context.Categories.ToList();
			List<DBModels.Task> tasks = (from t in Tasks.ToList()
										 join c in categories
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

			if (categoryName != null)
			{
				tasks = tasks.Where(t => t.Category?.Name == categoryName).ToList();
			}

			switch (status)
			{
				case "active":
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

				case "deadline":
				default:
					List<DBModels.Task> sortedTasks = tasks.Where(t => !t.Expired).OrderBy(t => t.Deadline).ToList();
					sortedTasks.AddRange(tasks.Where(t => t.Expired).OrderBy(t => t.Deadline).ToList());
					tasks = sortedTasks;
					break;
			}
			return tasks;
		}

		public void RemoveTask(DBModels.Task task)
		{
			_context.Tasks.Remove(task);
			_context.SaveChanges();
		}

		public void UpdateTask(DBModels.Task task)
		{
			DBModels.Task? oldTask = _context.Tasks.FirstOrDefault(t => t.Id == task.Id);
			if (oldTask != null)
			{
				oldTask.Content = task.Content;
				oldTask.Deadline = task.Deadline;
				oldTask.Expired = task.Expired;
				oldTask.CategoryId = task.CategoryId;
				oldTask.Category = task.Category;
			}
			_context.SaveChanges();
		}
	}
}
