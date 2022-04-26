namespace MVCapp.ViewModels
{
	public class SidebarSectionItemViewModel
	{
		public string Name { get; set; }
		public string IconClassList { get; set; }
		public string ClassName { get; set; }
		public Dictionary<string, string> QueryString { get; set; }

		public SidebarSectionItemViewModel(string name, string iconClassList)
		{
			Name = name;
			IconClassList = iconClassList;
		}

		public static SidebarSectionItemViewModel CategoriesSectionItemAll = new SidebarSectionItemViewModel("All", "fa-solid fa-globe");
	}
}
