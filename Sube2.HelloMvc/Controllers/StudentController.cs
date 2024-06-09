using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sube1.HelloMVC.Models;

namespace Sube1.HelloMVC.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            using (var ctx = new OkulDbContext())
            {
                var lst = ctx.Ogrenciler.ToList();
                return View(lst);
            }
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Ogrenci ogrenci)
        {
            if (ogrenci != null)
            {
                using (var ctx = new OkulDbContext())
                { 
                    ctx.Ogrenciler.Add(ogrenci);
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult EditStudent(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var ogr = ctx.Ogrenciler.Find(id);
                return View(ogr);
            }

        }
        [HttpPost]
        public IActionResult EditStudent(Ogrenci ogrenci)
        {

            if (ogrenci != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Entry(ogrenci).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteStudent(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                ctx.Ogrenciler.Remove(ctx.Ogrenciler.Find(id));
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult StudentLessons(int studentId)
        {
            using (var ctx = new OkulDbContext())
            {
                var studentLessons = ctx.OgrenciDersler
                .Where(od => od.Ogrenciid == studentId).Join(ctx.Ogrenciler, od => od.Ogrenciid, o => o.Ogrenciid, (od, o) => new { od, o })
                .Join(ctx.Dersler, od_o => od_o.od.Dersid, d => d.Dersid,
                    (od_o, d) => new StudentLessonViewModel
                    {
                        StudentId = od_o.o.Ogrenciid,
                        StudentName = od_o.o.Ad,
                        StudentSurname = od_o.o.Soyad,
                        LessonId = d.Dersid,
                        LessonName = d.Dersad,
                        LessonCredit = d.Kredi
                    })
                .ToList();

                return View(studentLessons);
            }
        }
    }
}
