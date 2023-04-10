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


      // uygulaman�n hangi servisler ile aya�a kalkt���

      var app = builder.Build();

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
      app.UseLogging(); // MVC5 taraf�nda bu middleware benzer bir yap�yo ActionFilter Attribuleri GlobalFilterCollection vasatas� ile yapt�k. 

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthorization();
      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");

      app.Run();// run middleware i�inde next yok o y�zden s�re� burada sonan�p uygulama run ediliyor.
      // run alt�nda hi� bir middleware �al��t�ramay�z.

      //app.Use(async (context, next) =>
      //{
      //  context.Response.Redirect("");
      //});
    }
  }
}