namespace NetCoreIntro.DIServices
{
  public class SingletonService : MicrosoftDI
  {
    public string Id { get; set; }

    public SingletonService()
    {
      Id = Guid.NewGuid().ToString();
    }
  }
}
