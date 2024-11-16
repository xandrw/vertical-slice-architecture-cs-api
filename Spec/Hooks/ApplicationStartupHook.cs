namespace Spec.Hooks;

using System.Threading.Tasks;
using TechTalk.SpecFlow;

// [Binding]
public static class ApplicationStartupHook
{
    private static Application? _application;

    [BeforeTestRun]
    public static void SetupApplication()
    {
        // var setupApplicationTask = Application.StartNewAsync();
        // setupApplicationTask.Wait();
        //
        // _application = setupApplicationTask.Result;
    }

    [AfterTestRun]
    public static void DestroyApplication()
    {
        // if (_application is not null)
        // {
        //     Task.Run(_application.DisposeAsync).Wait();
        // }
    }
}