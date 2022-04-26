namespace MVCapp.ViewModels
{
	public class SidebarViewModel
	{
		public Dictionary<string, SidebarSectionViewModel> SidebarSections;

		public SidebarViewModel(Dictionary<string, SidebarSectionViewModel> sidebarSections)
		{
			SidebarSections = sidebarSections;
		}

		public void SetItemsQueryStrings(IQueryCollection query)
		{
			foreach (KeyValuePair<string, SidebarSectionViewModel> section in SidebarSections)
			{
				foreach (KeyValuePair<string, SidebarSectionItemViewModel> sectionItem in section.Value.Items)
				{
					sectionItem.Value.QueryString = new Dictionary<string, string>();
					foreach (KeyValuePair<string, SidebarSectionViewModel> item in SidebarSections)
					{
						if (item.Key != section.Key)
							sectionItem.Value.QueryString.Add(item.Key, query[item.Key].ToString());
						else if (section.Value.SectionItemDefaultName != sectionItem.Key)
							sectionItem.Value.QueryString.Add(section.Key, sectionItem.Key);
					}
				}
			}
		}
	}
}
