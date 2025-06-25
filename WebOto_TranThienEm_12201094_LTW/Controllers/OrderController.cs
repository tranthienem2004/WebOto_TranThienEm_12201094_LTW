using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebOto_TranThienEm_12201094_LTW.Models;
using WebOto_TranThienEm_12201094_LTW.Models.Data;

namespace WebOto_TranThienEm_12201094_LTW.Controllers
{
    public class OrderController : Controller
    {
        private readonly CarDbContext _context;

        public OrderController(CarDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Car)
                    .ThenInclude(c => c.Brand)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                TempData["Error"] = "Xe không tồn tại!";
                return RedirectToAction("Index", "Car");
            }

            var order = new Order
            {
                CarId = car.Id
            };

            ViewBag.Car = car;
            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("CarId,FullName,Phone,Email,Address,Note")] Order order)
        {
            if (order.CarId == 0 && Request.Form.ContainsKey("CarId"))
            {
                int.TryParse(Request.Form["CarId"], out var carId);
                order.CarId = carId;
            }

            if (ModelState.IsValid)
            {
                var car = await _context.Cars.FindAsync(order.CarId);
                if (car == null)
                {
                    TempData["Error"] = "Xe không tồn tại!";
                    return RedirectToAction("Index", "Car");
                }

                order.OrderDate = DateTime.Now;
                order.Status = OrderStatus.Pending;

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Đặt hàng thành công!";
                return RedirectToAction("Success", new { id = order.Id });
            }

            foreach (var kvp in ModelState)
            {
                foreach (var error in kvp.Value.Errors)
                {
                    Console.WriteLine($"❌ ModelState error: {kvp.Key} - {error.ErrorMessage}");
                }
            }

            var carInfo = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == order.CarId);

            ViewBag.Car = carInfo;
            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> Success(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Car)
                    .ThenInclude(c => c.Brand)
                .Include(o => o.Car)
                    .ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng!";
                return RedirectToAction("Index", "Car");
            }

            return View(order);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                TempData["Error"] = "Không tìm thấy xe!";
                return RedirectToAction("Index");
            }

            return View(car);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, string cancelReason)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng!";
                return RedirectToAction("Index");
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                TempData["Warning"] = "Đơn hàng đã được hủy!";
                return RedirectToAction("Details", new { id });
            }

            if (order.Status == OrderStatus.Completed)
            {
                TempData["Error"] = "Không thể hủy đơn hàng đã hoàn thành!";
                return RedirectToAction("Details", new { id });
            }

            order.Status = OrderStatus.Cancelled;
            order.CancelledDate = DateTime.Now;
            order.CancelReason = !string.IsNullOrEmpty(cancelReason) ? cancelReason : "Khách hàng yêu cầu hủy";

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đơn hàng đã được hủy!";
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return RedirectToAction("Index");

            var orders = await _context.Orders
                .Include(o => o.Car)
                    .ThenInclude(c => c.Brand)
                .Where(o => o.Email.Contains(searchTerm) || o.Phone.Contains(searchTerm) || o.FullName.Contains(searchTerm))
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            ViewBag.SearchTerm = searchTerm;
            return View("Index", orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Car)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng!";
                return RedirectToAction("Index");
            }

            if (order.Status == OrderStatus.Confirmed || order.Status == OrderStatus.Completed)
            {
                TempData["Warning"] = "Đơn hàng đã được xác nhận hoặc đã hoàn thành!";
                return RedirectToAction("Details", new { id });
            }

            order.Status = OrderStatus.Confirmed;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đơn hàng đã được xác nhận!";
            return RedirectToAction("Details", new { id });
        }
    }
}