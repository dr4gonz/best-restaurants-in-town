using Nancy;
using /*NAMESPACE.OBJECTS*/;

namespace /*NAMESPACE*/
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] View["index.cshtml"];
    }
  }

}
