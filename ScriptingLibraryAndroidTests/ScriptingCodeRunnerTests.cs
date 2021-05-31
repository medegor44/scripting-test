using NUnit.Framework;
using System.Threading.Tasks;
using ScriptingLibrary;

namespace ScriptingLibraryAndroidTests
{
    [TestFixture]
    public class ScriptingCodeRunnerTests
    {
        [Test]
        public async Task RunSucceed()
        {
            var code = "var a = 1; var b = 2; var c = a + b;";

            var runner = new ScriptCodeRunner(code, new Container());

            await runner.RunAsync();
        }
    }
}