using Microsoft.AspNetCore;
using NetCoreIntro.DIServices;
using NetCoreIntro.Middlewares;
using NetCoreIntro.Services;

namespace NetCoreIntro
{

  // EF Core (CRUD operations)
  // Custom Tag Helper
  // Custom View Component
  // JSON Result, AJAX i�lemleri JQUERY
  // CLientside paket y�ntemi
  // Session (InMemory)
  // Cookie


  // NTier Katmanl� Clean Architecture Proje Yap�s� (1 saat)
  // EF Core Identity Yap�s�
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

  // Not: Form �zerinden IForm interface 

  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);


      #region IoContainer�slemleri


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
      // IEmailService hangi s�n�f ile �al��aca��n� s�yledim.
      // builder.Services.AddTransient<IEmailService, TurkcellEmailService>(); // uygulama genelinde her �a��rld���nda instance al�r, klas�rleri a�ma �rne�i
      //builder.Services.AddSingleton(); // uygulama genelinde tek instance �al���r, ara� �ubu�u
      //builder.Services.AddScoped(); // web request bazl� �al���r, controller bir istek att���m�zda oradaki servisi genelde bununla tan�mlar�z.
      // add ile register i�lemi ger�ekle�iyor.

      // config dosyas� �zerinden duruma g�re service �al��t�rma

     var emailProvider = builder.Configuration.GetSection("EmailSettings").GetValue<string>("Provider");

      if(emailProvider == "Turkcell")
      {
        builder.Services.AddTransient<IEmailService, TurkcellEmailService>();
      }
      else
      {
        builder.Services.AddTransient<IEmailService, NetSmtpEmailService>();
      }

      // config dosyas� �zerinden dependecy injection y�ntemi.


      #endregion

      #region MicrosoftDI

      // Transient her istekte instance al�r
      builder.Services.AddTransient<TransientService>();

      // her bir controller request de birden fazla kez �a��rlsa dahi tek bir instance al�r
      builder.Services.AddScoped<ScopeService>();

      // singleton ise uygulama genelinde tek bir instance ile �al���r
      builder.Services.AddSingleton<SingletonService>();

      #endregion

      #region Middlewares

      // uygulaman�n hangi servisler ile aya�a kalkt���

      var app = builder.Build(); // web uyglamas� application olarak aya�a kalk�yor.
                                 // application hangi ara yaz�l�mlara ile s�reci i�yecek.

      // uygulama aya�a kalkarken hangi hizmetleri �al��t�rd���
      // middleware (ara yaz�l�m)

      // Configure the HTTP request pipeline.
      if (!app.Environment.IsDevelopment())
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }


      // middleware gelen istek request response aras�na girip ilgili i�lemlerin yap�lmas�n� sa�lar.
      // 2 farkl� middleware var useMiddleware biri runMiddleware
      //app.Use(async (context, next) =>
      //{
      //  var request = context.Request.Path;
      //  var requestMethod = context.Request.Method;
      //  var isHttps = context.Request.IsHttps;

      //  //var session = context.Session.GetString("deneme");
      //  // core bir web yap�s�na sahip oldu�u i�in �uan default da uygulama session servisini kullanm�yor bunu bile ihtiya� haline tan�mlay�p uygulamay� aya�a kald�rabiliriz.
      //  var jsonObject = new { Id = 1, Name = "Deneme" };

      //   await context.Response.WriteAsJsonAsync(jsonObject);


      // // logic i�liyor
      //  await next(); // next method ile di�er middleware ge�
      //  // s�re� bir sonraki ara yaz�lma devrediliyor.

      //});

      // kendi yazd���m�z middleware �a��rd�m�z k�s�m.
      //app.UseMiddleware<LoggingMiddleware>();
      //app.UseLogging(); // MVC5 taraf�nda bu middleware benzer bir yap�yo ActionFilter Attribuleri GlobalFilterCollection vasatas� ile yapt�k. 

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthorization();

      // uygulama art�k area ile �al��abilir.
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

      app.Run();// run middleware i�inde next yok o y�zden s�re� burada sonan�p uygulama run ediliyor.
                // run alt�nda hi� bir middleware �al��t�ramay�z.

      //app.Use(async (context, next) =>
      //{
      //  context.Response.Redirect("");
      //});

      #endregion
    }
  }
}