using Microsoft.AspNetCore.Mvc;

namespace NetCoreIntro.Areas.Admin.Controllers
{
  // Admin Home Controller olduğunu anlayıp, uygulamada 2 farklı Controller var diye hata vermez
  [Area("Admin")] // area attribute ile hangi areanın controller olduğunu belirttik.
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
