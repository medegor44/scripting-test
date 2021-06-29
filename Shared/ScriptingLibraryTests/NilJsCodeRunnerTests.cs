using System;
using HelperObjects;
using ScriptingLibrary;
using Xunit;

namespace ScriptingLibraryTests
{
    public class NilJsCodeRunnerTests
    {
        [Fact]
        public async void Run()
        {
            var code = "let a = 1;";
            var runner = new NilJsCodeRunner(code, new Container());
            await runner.RunAsync();
        }

        [Fact]
        public async void RunWithGlobalVariables_Succeed()
        {
            var code = @"
let a = 1; 
a += t.x; 
if (a !== 2)
    throw new Error()";

            var obj = new HelperObject { x = 1 };

            var container = new Container();
            container.Register(obj, "t");

            var runner = new NilJsCodeRunner(code, container);

            await runner.RunAsync();
        }

        [Fact]
        public async void RunWithPrimiriveGlobalVariables_Succeed()
        {
            var code = @"
let a = 1; 
a += t; 
if (a !== 2)
    throw new Error()";

            var container = new Container();
            container.Register(1, "t");

            var runner = new NilJsCodeRunner(code, container);

            await runner.RunAsync();
        }

        [Fact]
        public async void ExternalFuncWillCall()
        {
            var code = "f()";
            int cnt = 0;

            var container = new Container();
            container.Register<Action>(() =>
            {
                cnt++;
            }, "f");

            var runner = new NilJsCodeRunner(code, container);

            await runner.RunAsync();

            Assert.Equal(1, cnt);
        }
    }
}