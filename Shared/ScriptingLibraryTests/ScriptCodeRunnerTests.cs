using Xunit;
using ScriptingLibrary;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Scripting;
using HelperObjects;
using System;

namespace ScriptingLibraryTests
{
    public class ScriptCodeRunnerTests
    {
        [Fact]
        public async Task CorrectScriptCode_ExecutionSucceed()
        {
            var code = "var a = 1; var b = 2; var c = a + b;";

            var runner = new ScriptCodeRunner(code, new Container());

            await runner.RunAsync();
        }

        [Fact]
        public async Task RunWithGlobalVariables_Succeed()
        {
            var container = new Container();

            var t = new HelperObject();

            container.Register(t, "t");

            var code = "t.x++;";

            var runner = new ScriptCodeRunner(code, container);
            await runner.RunAsync();

            Assert.Equal(1, t.x);
        }

        [Fact]
        public async Task RunWithFuncFromContainer_Succeed()
        {
            var container = new Container();

            int x = 0;
            container.Register<Action>(() => { x++; }, "t");
            //container.Register<Console>("api");

            var code = "t();";

            var runner = new ScriptCodeRunner(code, container);
            await runner.RunAsync();

            Assert.Equal(1, x);
        }

        
        [Fact]
        public async Task RunWithNotAllowedFunc_Fail()
        {
            var container = new Container();
            
            var code = "var w = new System.IO.StreamWriter(\"a.txt\"); w.WriteLine(\"Hello\"); w.Close();";

            var runner = new ScriptCodeRunner(code, container);

            await Assert.ThrowsAsync<CompilationErrorException>(() => runner.RunAsync());
        }

        [Fact]
        public async Task InvalidScriptCode_ThrowsAnException()
        {
            var code = "vat a = 1;";

            var runner = new ScriptCodeRunner(code, new Container());

            await Assert.ThrowsAsync<CompilationErrorException>(() => runner.RunAsync());
        }
    }
}
