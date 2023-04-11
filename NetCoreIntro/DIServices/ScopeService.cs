namespace NetCoreIntro.DIServices
{
  public class ScopeService : MicrosoftDI
  {
    public string Id { get; set; }

    public ScopeService()
    {
      Id = Guid.NewGuid().ToString();
    }
  }
}
