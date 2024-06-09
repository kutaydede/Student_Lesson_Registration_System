using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sube1.HelloMVC.Models;

namespace Sube1.HelloMVC.Controllers
{
    public class LessonRegistrationController : Controller
    {
        public IActionResult Index(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var ogrenciDersler = ctx.OgrenciDersler.Where(od => od.Ogrenciid == id).ToList();
                return View(ogrenciDersler);
            }

        }
        [HttpGet]
        public IActionResult AddLessonRegistration(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var lst = ctx.Dersler.ToList();
                ViewBag.StudentId = id;
                return View(lst);
            }
        }
        [HttpPost]
        public IActionResult AddLessonRegistration(int ogrenciid, int dersid)
        {
            var ogrenciDers = new OgrenciDers();

            using (var ctx = new OkulDbContext())
            {
                bool exists = ctx.OgrenciDersler.Any(od => od.Ogrenciid == ogrenciid && od.Dersid == dersid);
                if (!exists)
                {
                    ogrenciDers.Dersid = dersid;
                    ogrenciDers.Ogrenciid = ogrenciid;

                    ctx.OgrenciDersler.Add(ogrenciDers);
                    ctx.SaveChanges();
                }
                else
                {                 
                    TempData["ErrorMessage"] = "Bu öğrenci zaten bu derse kayıtlı.";
                }
            }
            return RedirectToAction("AddLessonRegistration", new { id = ogrenciDers.Ogrenciid });
        }



    }
}
