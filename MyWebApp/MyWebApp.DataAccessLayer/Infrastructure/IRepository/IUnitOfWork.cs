namespace MyWebApp.DataAccessLayer.Infrastructure.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category{ get; }
        IProductRepository Product{ get; }
        IAppUserRepository AppUser{ get; }
        ICartRepository  Cart{ get; }
        IOrderDetailRepository OrderDetail{ get; }
        IOrderHeaderRepository OrderHeader{ get; }

         void Save();
    }
}
    