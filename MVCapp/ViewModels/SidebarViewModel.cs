using Microsoft.Extensions.Primitives;
using MVCapp.DBModels;
using MVCapp.Models;

namespace MVCapp.ViewModels
{
	public class SidebarViewModel
	{
		private readonly string DefaultActiveSidebarItemName = "active";

		public List<Category> Categories { get; set; }

		public Dictionary<string, string> QueryCollection { get; set; }

		public SidebarViewModel(ICategoryRepository repository, IQueryCollection query)
		{
			Categories = repository.Categories.ToList();
			QueryCollection = GetQueryCollection(query);
		}

		public string IsActive(string sidebarSectionItemName, string sidebarSectionName)
		{
			if (QueryCollection.ContainsKey(sidebarSectionName))
			{
				return (QueryCollection[sidebarSectionName] == sidebarSectionItemName) ? DefaultActiveSidebarItemName : string.Empty;
			}
			else
				return (sidebarSectionItemName == string.Empty) ? DefaultActiveSidebarItemName : string.Empty;
		}

		public string IsCategoryActive(string? categoryName, string? selectedCategory) => (selectedCategory == categoryName) ? "active" : string.Empty;

		public Dictionary<string, string> GetQueryCollection(IQueryCollection query)
		{
			Dictionary<string, string> queryCollection = new Dictionary<string, string>();
			foreach (KeyValuePair<string, StringValues> queryItem in query)
			{
				queryCollection[queryItem.Key] = queryItem.Value;
			}
			return queryCollection;
		}

		public Dictionary<string, string> UpdateQueryCollection(string sidebarSectionItemName, string sidebarSectionName)
		{
			Dictionary<string, string> queryCollection = new Dictionary<string, string>(QueryCollection);
			queryCollection[sidebarSectionName] = sidebarSectionItemName;
			return queryCollection;
		}
	}
}
