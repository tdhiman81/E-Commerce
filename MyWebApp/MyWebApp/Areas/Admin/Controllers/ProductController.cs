using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebApp.DataAccessLayer.Infrastructure.IRepository;
using MyWebApp.Models.ViewModel;
using OfficeOpenXml.Table;
using OfficeOpenXml;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitofwork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }
        #region API
        public IActionResult AllProducts()
        {
            var products = _unitofwork.Product.GetAll(includeProperties: "Category");
            return Json(new { data = products });
        }
        #endregion

        public IActionResult Index()
        {
            //ProductVM productVM = new ProductVM();
            //productVM.Products = _unitofwork.Product.GetAll();
            return View();
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitofwork.Category.Add(category);
        //        _unitofwork.Save();
        //        TempData["success"] = "Category Created Done!!!";
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            ProductVM vm = new ProductVM()
            {
                Product = new(),
                Categories = _unitofwork.Category.GetAll().Select(x =>
                new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {

                vm.Product = _unitofwork.Product.GetT(x => x.Id == id);

                if (vm.Product == null)
                {
                    return NotFound();
                }
            }
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string fileName = String.Empty;
                if (file != null)
                {
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImage");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(path, fileName);

                    if (vm.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, vm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    vm.Product.ImageUrl = @"\ProductImage\" + fileName;

                }
                if (vm.Product.Id == 0)
                {
                    _unitofwork.Product.Add(vm.Product);
                    TempData["success"] = "Product Created Done!!!";
                }
                else
                {
                    _unitofwork.Product.Update(vm.Product);
                    TempData["success"] = "Product Updated Done!!!";
                }
                _unitofwork.Save();


                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");

        }
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{

        //    var category = _unitofwork.Category.GetT(x => x.Id == id);

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);
        //}

        #region
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var product = _unitofwork.Product.GetT(x => x.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error in Fetching Data" });
            }
            else
            {

                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _unitofwork.Product.Delete(product);
                _unitofwork.Save();

                return Json(new { success = true, message = "Product Deleted Successfully" });
            }

        }
        #endregion
        [HttpGet]
        public IActionResult DownloadReport()
        {
            return View();
        }


        [HttpPost]
        public IActionResult DownloadReport(IFormCollection obj)
        {
            string reportname = $"User_Wise_{Guid.NewGuid():N}.xlsx";
            var list = _unitofwork.Product.GetUserWiseReport();
            if (list.Count > 0)
            {
                var exportbytes = ExporttoExcel<Product>(list, reportname);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
            } 
            else
            {
                TempData["Message"] = "No Data to Export";
                return View();
            }
          
        }

        private byte[] ExporttoExcel<T>(List<T> table, string filename)
        {
            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromCollection(table, true, TableStyles.Light1);
            return pack.GetAsByteArray();
        }
    }

       
      
}
