using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sube1.HelloMVC.Models;

namespace Sube1.HelloMVC.Controllers
{
    public class LessonController : Controller
    {
        public IActionResult Index()
        {
            using (var ctx = new OkulDbContext())
            {
                var lst = ctx.Dersler.ToList();
                return View(lst);
            }
        }
        [HttpGet]
        public IActionResult AddLesson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLesson(Ders ders)
        {
            if (ders != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Dersler.Add(ders);
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult EditLesson(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var ders = ctx.Dersler.Find(id);
                return View(ders);
            }

        }
        [HttpPost]
        public IActionResult EditLesson(Ders ders)
        {

            if (ders != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Entry(ders).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteLesson(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                ctx.Dersler.Remove(ctx.Dersler.Find(id));
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
