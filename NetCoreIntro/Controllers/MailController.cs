using Microsoft.AspNetCore.Mvc;
using NetCoreIntro.Models;
using NetCoreIntro.Services;

namespace NetCoreIntro.Controllers
{
  public class MailController : Controller
  {
    // private NetSmtpEmailService NetSmtpEmailService = new NetSmtpEmailService();
    //private TurkcellEmailService TurkcellEmailService = new TurkcellEmailService();

    //private TurkcellEmailService TurkcellEmailService;
    //private NetSmtpEmailService NetSmtpEmailService;

    //public MailController()
    //{
    //  var mailController = new MailController(new TurkcellEmailService());
    //  mailController.SendMail();
    //}

    //public MailController(TurkcellEmailService turkcellEmailService)
    //{
    //  turkcellEmailService = turkcellEmailService;
    //}

    //public MailController(NetSmtpEmailService netSmtpEmailService)
    //{
    //  NetSmtpEmailService = netSmtpEmailService;
    //}


    private IEmailService emailService; // IEmailService implemente olan herhagi bir email service ile çalışabiliriz.
    private IConfiguration configuration;
    // configuration dosyasına controller üzerinden ulaşmak için
    // Not IConfiguration Interface ConfigurationManager sınıfını döndürür ve sistem tarafından AddControllersWithViews ile service yüklenir herhangi bir registerationa ihtiyacınız yok.

    public MailController(IEmailService emailService, IConfiguration configuration)
    {
      this.emailService = emailService;
      this.configuration = configuration;

      //var m = new MailController(new NetSmtpEmailService());
      //var m2 = new MailController(new TurkcellEmailService());
      //var m3 = new MailController(new ErrorViewModel());
    }

    /// <summary>
    /// Net Core tarafında Route[("route-key")] attribute yerine [HttpGet("route-key")]
    /// Attribute routing yaparken herhangi bir config gerek yok
    /// </summary>
    /// <returns></returns>
    [HttpGet("send-email")]
    public IActionResult SendMail()
    {
      //NetSmtpEmailService.SendEmail("Mesaj");
      //TurkcellEmailService.SendEmail("Mesaj");

      this.emailService.SendEmail("Mesaj1");
      ViewBag.Provider = this.configuration.GetSection("EmailSettings").GetValue<string>("Provider");

      return View();
    }
  }
}
