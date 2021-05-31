using Android.App;
using Android.OS;
using System.Reflection;
using Xamarin.Android.NUnitLite;

namespace ScriptingLibraryAndroidTests
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : TestSuiteActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            // tests can be inside the main assembly
            this.AddTest(Assembly.GetExecutingAssembly());
            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);
        }
    }
}