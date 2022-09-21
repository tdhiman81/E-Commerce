using MyWebApp.DataAccessLayer.Data;
using MyWebApp.DataAccessLayer.Infrastructure.IRepository;
using MyWebApp.Models;

namespace MyWebApp.DataAccessLayer.Infrastructure.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext _context;
        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public void Update(Category category)
        //{
        //    var categorydb = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
        //    if (categorydb != null)
        //    {
        //        categorydb.Name = category.Name;
        //        categorydb.DisplayOrder = category.DisplayOrder;
        //    }
        //}

        public void Update(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
        }
    }
}
 