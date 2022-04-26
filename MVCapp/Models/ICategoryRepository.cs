using MVCapp.DBModels;

namespace MVCapp.Models
{
	public interface ICategoryRepository
	{
		IEnumerable<Category> Categories { get; }
	}
}
