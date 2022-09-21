using MyWebApp.DataAccessLayer.Data;
using MyWebApp.DataAccessLayer.Infrastructure.IRepository;
using MyWebApp.Models;

namespace MyWebApp.DataAccessLayer.Infrastructure.Repository
{
    public class AppUserRepository : Repository<ApplicationUser>, IAppUserRepository
    {
        private ApplicationDbContext _context;
        public AppUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}

