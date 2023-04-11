namespace NetCoreIntro.Services
{
  public class NetSmtpEmailService : IEmailService
  {
    public void SendEmail(string message)
    {
      File.AppendAllText("Log.txt", "NetSmtpEmailService");
    }
  }
}
