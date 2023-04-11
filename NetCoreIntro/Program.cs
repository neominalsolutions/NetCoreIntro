using Microsoft.AspNetCore;
using NetCoreIntro.DIServices;
using NetCoreIntro.Middlewares;
using NetCoreIntro.Services;

namespace NetCoreIntro
{

  // EF Core (CRUD operations)
  // Custom Tag Helper
  // Custom View Component
  // JSON Result, AJAX iþlemleri JQUERY
  // CLientside paket yöntemi
  // Session (InMemory)
  // Cookie


  // NTier Katmanlý Clean Architecture Proje Yapýsý (1 saat)
  // EF Core Identity Yapýsý
  // Authentication Authorization, CookieBasedAuthentication, Authorize Attribute, Policy Based,Role Based,Claim Based (Permission Based)
  // Docker
  // Redis

  // WEB API
  // Token Based Authentication
  // JWT
  // HTTP STATUS CODE
  // HTTVERBS
  // SWAGGER API DOCUMENTATION
  // CORS

  // Not: Form üzerinden IForm interface 

  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);


      #region IoContainerÝslemleri


      //       var builder2 =  WebHost.CreateDefaultBuilder(args)
      //.ConfigureAppConfiguration((hostingContext, config) =>
      //{
      //  config.SetBasePath(Directory.GetCurrentDirectory());
      //  config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      //  config.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
      //  config.AddEnvironmentVariables();
      //})
      ;

      // Add services to the container.
      builder.Services.AddControllersWithViews();
      //builder.Services.AddControllers();
      //builder.Services.AddRazorPages();
      //builder.Services.AddServerSideBlazor();

      // IoC Container IServiceCollection
      // IEmailService hangi sýnýf ile çalýþacaðýný söyledim.
      // builder.Services.AddTransient<IEmailService, TurkcellEmailService>(); // uygulama genelinde her çaðýrldýðýnda instance alýr, klasörleri açma örneði
      //builder.Services.AddSingleton(); // uygulama genelinde tek instance çalýþýr, araç çubuðu
      //builder.Services.AddScoped(); // web request bazlý çalýþýr, controller bir istek attýðýmýzda oradaki servisi genelde bununla tanýmlarýz.
      // add ile register iþlemi gerçekleþiyor.

      // config dosyasý üzerinden duruma göre service çalýþtýrma

     var emailProvider = builder.Configuration.GetSection("EmailSettings").GetValue<string>("Provider");

      if(emailProvider == "Turkcell")
      {
        builder.Services.AddTransient<IEmailService, TurkcellEmailService>();
      }
      else
      {
        builder.Services.AddTransient<IEmailService, NetSmtpEmailService>();
      }

      // config dosyasý üzerinden dependecy injection yöntemi.


      #endregion

      #region MicrosoftDI

      // Transient her istekte instance alýr
      builder.Services.AddTransient<TransientService>();

      // her bir controller request de birden fazla kez çaðýrlsa dahi tek bir instance alýr
      builder.Services.AddScoped<ScopeService>();

      // singleton ise uygulama genelinde tek bir instance ile çalýþýr
      builder.Services.AddSingleton<SingletonService>();

      #endregion

      #region Middlewares

      // uygulamanýn hangi servisler ile ayaða kalktýðý

      var app = builder.Build(); // web uyglamasý application olarak ayaða kalkýyor.
                                 // application hangi ara yazýlýmlara ile süreci iþyecek.

      // uygulama ayaða kalkarken hangi hizmetleri çalýþtýrdýðý
      // middleware (ara yazýlým)

      // Configure the HTTP request pipeline.
      if (!app.Environment.IsDevelopment())
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }


      // middleware gelen istek request response arasýna girip ilgili iþlemlerin yapýlmasýný saðlar.
      // 2 farklý middleware var useMiddleware biri runMiddleware
      //app.Use(async (context, next) =>
      //{
      //  var request = context.Request.Path;
      //  var requestMethod = context.Request.Method;
      //  var isHttps = context.Request.IsHttps;

      //  //var session = context.Session.GetString("deneme");
      //  // core bir web yapýsýna sahip olduðu için þuan default da uygulama session servisini kullanmýyor bunu bile ihtiyaç haline tanýmlayýp uygulamayý ayaða kaldýrabiliriz.
      //  var jsonObject = new { Id = 1, Name = "Deneme" };

      //   await context.Response.WriteAsJsonAsync(jsonObject);


      // // logic iþliyor
      //  await next(); // next method ile diðer middleware geç
      //  // süreç bir sonraki ara yazýlma devrediliyor.

      //});

      // kendi yazdýðýmýz middleware çaðýrdýmýz kýsým.
      //app.UseMiddleware<LoggingMiddleware>();
      //app.UseLogging(); // MVC5 tarafýnda bu middleware benzer bir yapýyo ActionFilter Attribuleri GlobalFilterCollection vasatasý ile yaptýk. 

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthorization();

      // uygulama artýk area ile çalýþabilir.
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );
      });

      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");

      app.Run();// run middleware içinde next yok o yüzden süreç burada sonanýp uygulama run ediliyor.
                // run altýnda hiç bir middleware çalýþtýramayýz.

      //app.Use(async (context, next) =>
      //{
      //  context.Response.Redirect("");
      //});

      #endregion
    }
  }
}