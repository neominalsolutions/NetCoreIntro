using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreIntro.Models;

namespace NetCoreIntro.Controllers
{
  public class FormController : Controller
  {

    public List<SelectListItem> GetCities
    {
      get
      {
        return new List<SelectListItem>{
          new SelectListItem
          {
            Text = "Ankara",
            Value = "06",
            Selected = true
          },
           new SelectListItem
           {
             Text = "İstanbul",
             Value = "34",
             Selected = false
           }
        };
      }
    }


    /// <summary>
    /// Sayfa form ekranı buradan load olsun
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Register()
    {
      ViewBag.Cities = GetCities;

      return View();
    }

    /// <summary>
    /// Formu submit ettikten sonra düşeceğimiz kısmı
    /// </summary>
    /// <returns></returns>

    [HttpPost]
    [ValidateAntiForgeryToken] // CSRF ataklarından korumak için
    public IActionResult Register(RegisterModel model)
    {
      ViewBag.Cities = GetCities;

      // validasyondan geçtiyse
      if (ModelState.IsValid)
      {
        // veri tabanına gönder
        ModelState.Clear(); // form bilgilerini temizle
        //ModelState.Select(x => x.Value).ToList();
      }
      else
      {
        // duruma göre kendimize bir hata döndürebiliriz.
        ModelState.AddModelError("Hata", "Form Hata");

        string[] errors = ModelState.Values.SelectMany(x => x.Errors).Select(a => a.ErrorMessage).ToList().ToArray();
        ;
      }

      return View();
    }
  }
}
