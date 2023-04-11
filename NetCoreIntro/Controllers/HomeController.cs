using Microsoft.AspNetCore.Mvc;
using NetCoreIntro.Models;
using System.Diagnostics;

namespace NetCoreIntro.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index(string RequestId = "12345")
    {
      

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [Route("categories",Name ="CategoryRoute")]
    public IActionResult Categories(int? id)
    {
      return View();
    }

    //RouteKey yani nami ile ilgili route değerini action'a tanımladık.
    [Route("/speakers", Name ="speakerRoute")]
    public IActionResult Spikers(string speakerId,string currentYear)
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