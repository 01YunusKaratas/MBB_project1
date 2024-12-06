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
        private readonly ApplicationDbContext _context; //bunu Data altındaki yerden buraya entegre ettim.
     


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
            var products = _context.product_table
            .OrderByDescending(y=>y.PRODUCT_ID) //productId ye göre azalana göre sıralar 
            .ToList();  
            
            // "Index1" görünümüne ürünleri gönderiyoruz
            return View(products);
        }

        //Kullanıcı Login Doğrulama İçin Bunu Yapcaz
        [HttpPost]
        public IActionResult Index2(User model){

            var user = _context.user_table.Where(x => x.USERNAME == model.USERNAME && x.PASSWORD == model.PASSWORD).FirstOrDefault();

            if (user != null){
                
                HttpContext.Session.SetString("IsActive", model.ID.ToString());
                Console.WriteLine($"{user.ID}{user.USERNAME}{user.PASSWORD}");
                ViewBag.message = "Login is Successfully";
                return RedirectToAction("Index1");  
            }else{
                ViewBag.Errors = "Girdiğiniz kullanıcı adı veya şifre yanlış. Lütfen bilgilerinizi kontrol ederek tekrar deneyin.";
                return View("Index");
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int PRODUCT_ID ){

            var delete = _context.product_table.FirstOrDefault(a => a.PRODUCT_ID == PRODUCT_ID);
            //yukarıda yazdığımız sorgu LINQ sorgusu ile yazılmış bir lambda sorgusudur.
             
             if(delete != null){
                _context.product_table.Remove(delete);//burada yine constructor ile database gidip kitap tablosundan ilk gelen kitapı silcez
                _context.SaveChanges();//sonra kayıtedcez.
                Console.WriteLine($"Ürün {PRODUCT_ID} başarıyla silindi.");
             }
          
            
            return RedirectToAction("Index1","Home");
             
                     
        }

        public IActionResult Added(Product model){

                if(ModelState.IsValid){ //Herzaman olması lazım

                    _context.product_table.Add(model);//ürünü ekledik
                    _context.SaveChanges();
                    return RedirectToAction("Index1","Home");
                }

            return View(model);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOut(){

           HttpContext.Session.Clear();//Burada kullanıcı çıkınca sessionı temizlemiş olduk.
            return RedirectToAction("Index", "Home");
        }

  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product model)
        {
            // Model geçerli mi diye kontrol ediyoruz
            if (ModelState.IsValid)
            {
                // Ürün veritabanında var mı kontrol ediyoruz
                var productToUpdate = _context.product_table.FirstOrDefault(p => p.PRODUCT_ID == model.PRODUCT_ID);

                if (productToUpdate != null)
                {
                    // Ürünü güncelliyoruz
                    productToUpdate.PRODUCT_NAME = model.PRODUCT_NAME;
                    productToUpdate.PRODUCT_COUNT = model.PRODUCT_COUNT;
                    productToUpdate.PRODUCT_DEPARTMENT = model.PRODUCT_DEPARTMENT;

                    // Değişiklikleri kaydediyoruz
                    _context.SaveChanges();
                }
            }
            
            // Eğer güncelleme başarılıysa, Index1 sayfasına yönlendiriyoruz
            return RedirectToAction("Index1", "Home");
        }
        
        //Burada filtreleme işlemleri için ayrıldı
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterForm(int? PRODUCT_ID ,string PRODUCT_NAME, int? PRODUCT_COUNT,string PRODUCT_DEPARTMENT){
            
            var productQuery = _context.product_table.AsQueryable();//Sadece db'den veri sorgularken gerekli bilgileri alır Efficient ve optimization sağalr

            if(PRODUCT_ID.HasValue){//burada int olduğu için .HasValue
                productQuery = productQuery.Where(y=>y.PRODUCT_ID==PRODUCT_ID.Value);//yani kullanıcıdan gelen ıd eşitmi
            }

            if(!string.IsNullOrEmpty(PRODUCT_NAME)){//String olduğunda ise IsNullorEmpty yapıyoruz
                //Normalde alttaki gibi içerirmi diye bakıyorduk ya büyük küçün harf duyarsızlığı için EF.functions.Lıke(y.productname diyoruz)
                //productQuery=productQuery.Where(y=>y.PRODUCT_NAME.Contains(PRODUCT_NAME));
                productQuery = productQuery.Where(y=>EF.Functions.Like(y.PRODUCT_NAME,$"%{PRODUCT_NAME}%"));//YANİ BU GİRDİĞİMİZ BİLGİYİ İÇERİYORMU
            }
            if(PRODUCT_COUNT.HasValue){
                productQuery = productQuery.Where(y=>y.PRODUCT_COUNT==PRODUCT_COUNT.Value);
            }
            if(!string.IsNullOrEmpty(PRODUCT_DEPARTMENT)){

                productQuery =productQuery.Where(y=>EF.Functions.Like(y.PRODUCT_DEPARTMENT,$"%{PRODUCT_DEPARTMENT}%"));
            }


            var filterQuery =productQuery.ToList(); //sORGULAMA SONUCU OLUŞAN herşeyi liste olarak ver




            return View("Index1",filterQuery);
            

        }
        


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
