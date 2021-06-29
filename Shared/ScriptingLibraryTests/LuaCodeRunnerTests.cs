using System;
using HelperObjects;
using ScriptingLibrary;
using Xunit;

namespace ScriptingLibraryTests
{
    public class LuaCodeRunnerTests
    {
        [Fact]
        public async void Run()
        {
            var code = "a = 1;";
            var runner = new LuaCodeRunner(code, new Container());
            await runner.RunAsync();
        }

        [Fact]
        public async void RunWithGlobalVariables_Succeed()
        {
            var code = @"
a = 1; 
a = a + t.x; 
if a ~= 2 then
    error()
end";

            var obj = new HelperObject { x = 1 };

            var container = new Container();
            container.Register(obj, "t");

            var runner = new LuaCodeRunner(code, container);

            await runner.RunAsync();
        }

        [Fact]
        public async void RunWithPrimitiveGlobalVariables_Succeed()
        {
            var code = @"
a = '1'; 
a = a + t; 
if a ~= '12' then
    error()
end";

            var container = new Container();
            container.Register("2", "t");

            var runner = new LuaCodeRunner(code, container);

            await runner.RunAsync();
        }

        [Fact(Skip = "until we found more elegant way to distinguish Action/Func from primitives and clsses")]
        public async void ExternalFuncWillCall()
        {
            var code = "f(2)";
            int cnt = 0;

            var container = new Container();
            container.Register<Action<int>>((int x) =>
            {
                cnt += x;
            }, "f");

            var runner = new LuaCodeRunner(code, container);

            await runner.RunAsync();

            Assert.Equal(2, cnt);
        }
    }
}