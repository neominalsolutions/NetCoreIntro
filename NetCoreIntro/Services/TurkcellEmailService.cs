namespace NetCoreIntro.Services
{
  public class TurkcellEmailService : IEmailService
  {
    public void SendEmail(string message)
    {
      File.AppendAllText("Log.txt", "TurkcellEmailService");
    }
  }
}
