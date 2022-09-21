using MyWebApp.DataAccessLayer.Data;
using MyWebApp.DataAccessLayer.Infrastructure.IRepository;
using MyWebApp.Models;

namespace MyWebApp.DataAccessLayer.Infrastructure.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _context;
        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public void Update(Category category)
        //{
        //    var categorydb=_context.Categories.FirstOrDefault(x=>x.Id==category.Id);
        //    if (categorydb!=null)
        //    {
        //        categorydb.Name = category.Name;
        //        categorydb.DisplayOrder = category.DisplayOrder;    
        //    }
        //}

        public void Update(OrderHeader orderHeader)
        {
            _context.OrderHeaders.Update(orderHeader);  
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var order = _context.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if(order != null)
            {
                order.OrderStatus= orderStatus;
            }
            if(paymentStatus != null)
            {
                order.PaymentStatus= paymentStatus;
            }
        }
    }
}
