using Task = MVCapp.DBModels.Task;

namespace MVCapp.ViewModels
{
	public class HomePageViewModel
	{
		public SidebarViewModel Sidebar { get; }

		public List<Task> Tasks { get; set; }

		public IQueryCollection URL { get; set; }

		public HomePageViewModel(IQueryCollection url, SidebarViewModel sidebar, List<Task> tasks)
		{
			URL = url;
			Sidebar = sidebar;
			Tasks = tasks;
		}

		public Dictionary<string, string> GetCategorySectionItemURL(string categoryName)
		{
			Dictionary<string, string> categoryHref = new Dictionary<string, string>();
			categoryHref = new Dictionary<string, string>();
			foreach (KeyValuePair<string, SidebarSectionViewModel> sidebarSectionItem in Sidebar.SidebarSections)
			{
				if (URL[sidebarSectionItem.Key].ToString() != sidebarSectionItem.Value.SectionItemDefaultName)
				{
					categoryHref.Add(sidebarSectionItem.Key, (sidebarSectionItem.Key == "category") ? categoryName : URL[sidebarSectionItem.Key].ToString());
				}
			}
			return categoryHref;
		}
	}
}
