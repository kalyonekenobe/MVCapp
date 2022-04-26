using MVCapp.DBModels;

namespace MVCapp.ViewModels
{
	public class CreateUpdateTaskViewModel
	{
		public List<Category> Categories { get; set; }

		public DBModels.Task? Task { get; set; }

		public CreateUpdateTaskViewModel(List<Category> categories)
		{
			Categories = categories;
		} 
	}
}
