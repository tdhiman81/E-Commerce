using MyWebApp.DataAccessLayer.Data;
using MyWebApp.DataAccessLayer.Infrastructure.IRepository;
using MyWebApp.Models;

namespace MyWebApp.DataAccessLayer.Infrastructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var categorydb=_context.Categories.FirstOrDefault(x=>x.Id==category.Id);
            if (categorydb!=null)
            {
                categorydb.Name = category.Name;
                categorydb.DisplayOrder = category.DisplayOrder;    
            }
        }
    }
}
