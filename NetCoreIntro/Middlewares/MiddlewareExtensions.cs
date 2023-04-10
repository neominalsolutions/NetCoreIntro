namespace NetCoreIntro.Middlewares
{
  public static class MiddlewareExtensions
  {
    /// <summary>
    /// IApplicationBuilder yeni bir özellik kazandırdık.
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseLogging(this IApplicationBuilder applicationBuilder)
    {
      return applicationBuilder.UseMiddleware<LoggingMiddleware>();
    } 

  }
}
