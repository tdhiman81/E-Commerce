


using Microsoft.EntityFrameworkCore;
using MyWebApp.DataAccessLayer.Data;
using MyWebApp.DataAccessLayer.Infrastructure.IRepository;
using MyWebApp.Models;

namespace MyWebApp.DataAccessLayer.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public void Update(Product product)
        {
            var productdb=_context.Products.FirstOrDefault(x=>x.Id==product.Id);
            if(productdb!=null)
            {
                productdb.Name=product.Name;
                productdb.Description = product.Description;
                productdb.Price= product.Price;
                if (product.ImageUrl != null)
                {
                    productdb.ImageUrl = product.ImageUrl;
                }
            }
        }
        public List<Product> GetUserWiseReport()
        {

            try
            {
                var listofusers = (from Product in _context.Products.AsNoTracking()
                                   select new Product()
                                   {
                                       Name = Product.Name,
                                       Description = Product.Description,
                                       Price = Product.Price,
                                       ImageUrl = Product.ImageUrl,
                                       CategoryId = Product.CategoryId,
                                       Category=Product.Category,

                                   }).ToList();

                return listofusers;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
