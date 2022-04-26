namespace MVCapp.Models
{
	public class TaskRepository : ITaskRepository
	{
		private readonly ApplicationDBContext _context;

		public TaskRepository(ApplicationDBContext context)
		{
			_context = context;
		}

		public IEnumerable<DBModels.Task> Tasks => _context.Tasks;

		public void AddTask(DBModels.Task task)
		{
			_context.Tasks.Add(task);
			_context.SaveChanges(true);
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
