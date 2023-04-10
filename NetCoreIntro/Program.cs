using NetCoreIntro.Middlewares;

namespace NetCoreIntro
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddControllersWithViews();
      //builder.Services.AddControllers();
      //builder.Services.AddRazorPages();
      //builder.Services.AddServerSideBlazor();


      // uygulamanýn hangi servisler ile ayaða kalktýðý

      var app = builder.Build();

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
      app.UseLogging(); // MVC5 tarafýnda bu middleware benzer bir yapýyo ActionFilter Attribuleri GlobalFilterCollection vasatasý ile yaptýk. 

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthorization();
      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");

      app.Run();// run middleware içinde next yok o yüzden süreç burada sonanýp uygulama run ediliyor.
      // run altýnda hiç bir middleware çalýþtýramayýz.

      //app.Use(async (context, next) =>
      //{
      //  context.Response.Redirect("");
      //});
    }
  }
}