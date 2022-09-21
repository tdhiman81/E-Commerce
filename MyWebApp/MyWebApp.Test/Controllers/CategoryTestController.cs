using MyWebApp.Controllers;
using MyWebApp.DataAccessLayer.Data;
using MyWebApp.DataAccessLayer.Infrastructure.IRepository;
using MyWebApp.DataAccessLayer.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.Test.Controllers
{
    public class CategoryTestController
    {
        private readonly IUnitOfWork _unitofwork;
        private ApplicationDbContext _context;
        private CategoryController _categoryController;
        public CategoryTestController()
        {
            _unitofwork = new UnitOfWork(_context);

        }
    }
}
 