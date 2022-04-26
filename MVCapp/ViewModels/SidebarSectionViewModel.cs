namespace MVCapp.ViewModels
{
	public class SidebarSectionViewModel
	{
		public string SectionItemDefaultName { get; set; }
		public string Name { get; set; }
		public Dictionary<string, SidebarSectionItemViewModel> Items { get; set; }

		public SidebarSectionViewModel(string name, string defaultItemName, Dictionary<string, SidebarSectionItemViewModel> items)
		{
			Name = name;
			SectionItemDefaultName = defaultItemName;
			Items = new Dictionary<string, SidebarSectionItemViewModel>(items);
		}

		public static SidebarSectionViewModel ViewTasksSection()
		{
			return new SidebarSectionViewModel("Show tasks", "all", new Dictionary<string, SidebarSectionItemViewModel>() 
			{
				{ "all", new SidebarSectionItemViewModel("All", "fa-solid fa-calendar-days") },
				{ "current", new SidebarSectionItemViewModel("Current", "fa-solid fa-calendar-check") },
				{ "expired", new SidebarSectionItemViewModel("Expired", "fa-solid fa-calendar-xmark") }
			});
		}

		public static SidebarSectionViewModel OrderBySection()
		{
			return new SidebarSectionViewModel("Sort by", "datetime", new Dictionary<string, SidebarSectionItemViewModel>()
			{
				{ "datetime", new SidebarSectionItemViewModel("Deadline", "fa-solid fa-stopwatch") },
				{ "content", new SidebarSectionItemViewModel("Alphabet", "fa-solid fa-arrow-down-a-z") }
			});
		}
	}
}
