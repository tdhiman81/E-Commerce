using MyWebApp.DataAccessLayer.Data;
using MyWebApp.DataAccessLayer.Infrastructure.IRepository;

namespace MyWebApp.DataAccessLayer.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IAppUserRepository AppUser { get; private set; }
        public ICartRepository Cart { get; private set; }

        public IOrderDetailRepository OrderDetail { get; set; }

        public IOrderHeaderRepository OrderHeader { get; set; }

        public UnitOfWork(ApplicationDbContext context) 
        { 
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
            Cart = new CartRepository(context);
            AppUser = new AppUserRepository(context);
            OrderHeader = new OrderHeaderRepository(context);
            OrderDetail = new OrderDetailRepository(context);
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
