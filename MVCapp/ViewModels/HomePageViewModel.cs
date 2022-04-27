using Microsoft.Extensions.Primitives;
using Task = MVCapp.DBModels.Task;

namespace MVCapp.ViewModels
{
	public class HomePageViewModel
	{
		public SidebarViewModel Sidebar { get; }

		public List<Task> Tasks { get; set; }

		public Dictionary<string, string> QueryCollection { get; set; }

		public HomePageViewModel(IQueryCollection query, SidebarViewModel sidebar, List<Task> tasks)
		{
			QueryCollection = GetQueryCollection(query);
			Sidebar = sidebar;
			Tasks = tasks;
		}

		public Dictionary<string, string> GetQueryCollection(IQueryCollection query)
		{
			Dictionary<string, string> queryCollection = new Dictionary<string, string>();
			foreach (KeyValuePair<string, StringValues> queryItem in query)
			{
				queryCollection[queryItem.Key] = queryItem.Value;
			}
			return queryCollection;
		}

		public Dictionary<string, string> UpdateQueryCollection(string queryItemValue, string queryItemName)
		{
			Dictionary<string, string> queryCollection = new Dictionary<string, string>(QueryCollection);
			queryCollection[queryItemName] = queryItemValue;
			return queryCollection;
		}
	}
}
