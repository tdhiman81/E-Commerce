using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.DataAccessLayer.Infrastructure.IRepository
{
    public interface ICategoryRepository: IRepository<Category>
    {
        void Update(Category category);
    }
}
