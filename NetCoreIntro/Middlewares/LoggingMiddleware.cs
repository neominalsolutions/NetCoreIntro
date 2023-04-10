namespace NetCoreIntro.Middlewares
{
  // Path Protokol Method RequestBody LogFile.txt
  public class LoggingMiddleware
  {
    // loglamadan sonra süreç devam etsin diye bir kod çalıştırmam lazım bu sebeple RequestDelegate sınıfını kullanırız
    private readonly RequestDelegate next;

    public LoggingMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    /// <summary>
    /// Middlewareler InvokeAsync dediğimiz asenkron yani Tread bazlı çalışan, performans amaçlı bu şekilde yazılmız bir method ile logiclerimizi request içersinde araya girip çalıştırmamızı sağlar
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
      var path = httpContext.Request.Path;
      var method = httpContext.Request.Method;
      var protocol = httpContext.Request.Protocol;
      string routeParam = string.Empty;
      
      if(httpContext.Request.Method == HttpMethods.Get)
      {
        routeParam =  httpContext.Request.RouteValues?["name"]?.ToString();

      }

      var logMessage = $"{protocol} {path} => {method} => routeParam: {routeParam}";

      File.AppendAllText("Log.txt", logMessage);

      await next(httpContext); // yani isteğimi bir sonraki sürece aktar.
    }
  }
}
