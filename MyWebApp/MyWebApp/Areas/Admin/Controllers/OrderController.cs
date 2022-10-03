using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.CommonHelper;
using MyWebApp.DataAccessLayer.Infrastructure.IRepository;
using MyWebApp.DataAccessLayer.Infrastructure.Repository;
using MyWebApp.Models;
using MyWebApp.Models.ViewModel;
using System.Security.Claims;

namespace MyWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region API
        [HttpGet]
        public IActionResult AllOrders(string status)
        {
            IEnumerable<OrderHeader> orderHeaders;

            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");

            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                orderHeaders = _unitOfWork.OrderHeader.GetAll(x => x.ApplicationUserId == claims.Value);

            }   

            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(x => x.PaymentStatus == PaymentStatus.StatusPending);
                    break;

                case "approved":
                    orderHeaders = orderHeaders.Where(x => x.PaymentStatus == PaymentStatus.StatusApproved);
                    break;

                default:
                    break;
            }
 
            orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            return Json(new { data = orderHeaders });
        }
        #endregion 
        public IActionResult Index()
        {
            return View();

        }

        public IActionResult Orderdetails(int id)
        {
            OrderVM orderVM = new OrderVM()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetT(x => x.Id == id,
                includeProperties: "ApplicationUser"),
                OrderDetail=_unitOfWork.OrderDetail.GetAll(x=>x.Id == id, 
                includeProperties: "Product")
            };

            return View(orderVM);
        }

    }
}
