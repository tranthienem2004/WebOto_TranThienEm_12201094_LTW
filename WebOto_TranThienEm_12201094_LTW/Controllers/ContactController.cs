using Microsoft.AspNetCore.Mvc;
using WebOto_TranThienEm_12201094_LTW.Models;
using WebOto_TranThienEm_12201094_LTW.Models.Data;

namespace WebOto_TranThienEm_12201094_LTW.Controllers
{
    public class ContactController : Controller
    {
        private readonly CarDbContext _context;

        public ContactController(CarDbContext context)
        {
            _context = context;
        }

        // Hiển thị form liên hệ
        public IActionResult Index()
        {
            return View();
        }

        // Xử lý gửi form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(Contact model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedAt = DateTime.Now;
                _context.Contacts.Add(model);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Cảm ơn bạn đã liên hệ với chúng tôi!";
                return RedirectToAction("Thanks");
            }
            return View("Index", model);
        }

        public IActionResult Thanks()
        {
            return View();
        }
    }
}