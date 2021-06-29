using System;
using HelperObjects;
using ScriptingLibrary;
using Xunit;

namespace ScriptingLibraryTests
{
    public class LensCodeRunnerTests
    {
        [Fact]
        public async void Run()
        {
            var code = "let a = 1";
            var runner = new LensCodeRunner(code, new Container());
            await runner.RunAsync();
        }

        [Fact]
        public async void RunWithGlobalVariables_Succeed()
        {
            var code = @"
let a = 1 

let b = a + t.x

if b <> 2 then
    throw new Exception";

            var obj = new HelperObject { x = 1 };

            var container = new Container();
            container.Register(obj, "t");

            var runner = new LensCodeRunner(code, container);

            await runner.RunAsync();
        }

        [Fact]
        public async void RunWithPrimitiveGlobalVariables_Succeed()
        {
            var code = @"
let a = 1 
let b = a + t
if b <> 2 then
    print 1";

            var container = new Container();
            container.Register(1, "t");

            var runner = new LensCodeRunner(code, container);

            await runner.RunAsync();
        }

        [Fact]
        public async void ExternalFuncWillCall()
        {
            var code = "f 2";
            int cnt = 0;

            var container = new Container();
            container.Register<Action<int>>((x) =>
            {
                cnt += x;
            }, "f");

            var runner = new LensCodeRunner(code, container);

            await runner.RunAsync();

            Assert.Equal(2, cnt);
        }
    }
}