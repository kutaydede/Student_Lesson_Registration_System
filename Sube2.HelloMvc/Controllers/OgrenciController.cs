using Microsoft.AspNetCore.Mvc;
using Sube1.HelloMVC.Models;
using Sube1.HelloMVC.Models.ViewModels;

namespace Sube1.HelloMVC.Controllers
{
    public class OgrenciController : Controller
    {
        public ViewResult Index()//Action Metod
        {
            return View();
        }

        public ViewResult OgrenciDetay(int id)
        {
            var ogrt = new Ogretmen { Ad = "Osman", Soyad = "Yılmaz", Ogretmenid = 1 };

            Ogrenci ogr = null;
            if (id == 1)
            {
                ogr = new Ogrenci { Ad = "Ali", Soyad = "Veli", Numara = 123 };
            }
            else if (id == 2)
            {
                ogr = new Ogrenci { Ad = "Ahmet", Soyad = "Mehmet", Numara = 456 };
            }

            ViewData["ogrenci"] = ogr;
            ViewBag.student = ogr;
            ViewBag.teacher = ogrt;


            OgrViewModel vm = new OgrViewModel();
            vm.Ogretmen = ogrt;
            vm.Ogrenci = ogr;
            vm.Ders = new Ders { Dersid = 1, Dersad = "ProgTem", Kredi = 5 };

            return View(vm);
        }

        public ViewResult OgrenciListe()
        {
            var lst = new List<Ogrenci>();
            lst.Add(new Ogrenci { Ad = "Ali", Soyad = "Veli", Numara = 123 });
            lst.Add(new Ogrenci { Ad = "Ahmet", Soyad = "Mehmet", Numara = 456 });

            return View(lst);
        }
    }
}
//Controller'dan View'e Veri Taşıma Yöntemleri

//ViewData: Koleksiyon yapısıdır. Dictonary türünde koleksiyonlardır.
//Dictionary yapıları key-value pair'lerden oluşur.
//Keyler tekil olmalıdır.
//Keyler string, value'lar object türündedir.

//ViewBag: ViewData üstüne geliştirilmiş bir yapıdır. Arka planda ViewData koleksiyonunu kullanır.

//ViewModel