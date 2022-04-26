using MVCapp.DBModels;

namespace MVCapp.Models
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDBContext _context;

		public CategoryRepository(ApplicationDBContext context)
		{
			_context = context;
		}
		public IEnumerable<Category> Categories => _context.Categories;
	}
}
