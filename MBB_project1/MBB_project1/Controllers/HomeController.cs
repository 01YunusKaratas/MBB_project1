using System.Diagnostics;
using MBB_project1.Data;
using MBB_project1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MBB_project1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; //bunu Data alt�ndaki yerden buraya entegre ettim.

        public HomeController(ApplicationDbContext context)
        {
            _context = context;//burayada update ettik
        }
        //Json dosyaları  sadece tek bir kök içerir ondan dolayı tek bir tane root oluşturman lazım
        public IActionResult Index()
        {
           


            return View();
        }

       [HttpGet]  //Get isteği
       
        public IActionResult Index1()
        {
            // Tüm ürünleri veritabanından çekiyoruz
            var products = _context.product_table.ToList(); 
            // "Index1" görünümüne ürünleri gönderiyoruz
            return View("Index1", products);
        }

        //Kullanıcı Login Doğrulama İçin Bunu Yapcaz
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
