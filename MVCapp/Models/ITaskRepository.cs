namespace MVCapp.Models
{
	public interface ITaskRepository
	{
		IEnumerable<DBModels.Task> Tasks { get; }

		void AddTask(DBModels.Task task);

		void RemoveTask(DBModels.Task task);

		void UpdateTask(DBModels.Task task);

		List<DBModels.Task> GetTaskList(string? status, string? order, string? categoryName);
	}
}
