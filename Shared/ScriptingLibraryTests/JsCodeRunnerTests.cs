using System;
using Xunit;
using ScriptingLibrary;
using HelperObjects;

namespace ScriptingLibraryTests
{
    public class JsCodeRunnerTests
    {
        [Fact]
        public async void Run()
        {
            var runner = new JsCodeRunner("let a = 1;", new Container());
            await runner.RunAsync();
        }

        [Fact]
        public async void RunWithGlobalVariables_Succeed()
        {
            Action act = () => { };

            var container = new Container();
            container.Register(act, "act");

            var runner = new JsCodeRunner("act();", container);

            await runner.RunAsync();
        }
    }
}