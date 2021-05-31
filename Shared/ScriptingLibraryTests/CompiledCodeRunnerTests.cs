using Xunit;
using ScriptingLibrary;
using System.Threading.Tasks;
using HelperObjects;

namespace ScriptingLibraryTests
{
    public class CompiledCodeRunnerTests
    {
        [Fact]
        public async Task CorrectCode_ExecutionSucceed()
        {
            var code = "var a = 1; var b = 2; var c = a + b;";

            var runner = new CompiledCodeRunner(code, new Container());
            await runner.RunAsync();
        }

        [Fact]
        public async Task RunWithGlobalVariables_Succeed()
        {
            var t = new HelperObject();

            var container = new Container();
            container.Register(t, "t");

            var code = "t.x++;";

            var runner = new CompiledCodeRunner(code, container);
            await runner.RunAsync();

            Assert.Equal(1, t.x);
        }
    }
}
