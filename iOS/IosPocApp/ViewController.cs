using Foundation;
using System;
using UIKit;
using ScriptingLibrary;

namespace IosPocApp
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override async void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            var runner = new ScriptCodeRunner("var a = 1; var b = 2; var c = a + b;", new Container());
            
            await runner.RunAsync();
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}