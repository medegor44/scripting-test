using System;
using System.Threading.Tasks;
using NUnit.Framework;
using ScriptingLibrary;

namespace AndroidTests
{
    [TestFixture]
    public class ScriptCodeRunnerTests
    {
        [Test]
        public async Task Pass()
        {
            var runner = new ScriptCodeRunner("var a = 1;", new Container());
            await runner.RunAsync();
        }

        [Test]
        public async Task ResolveFromContainer_Succeed()
        {
            var cnt = 0;
            Action f = () => cnt++;

            var container = new Container();
            container.Register(f, "f");

            var code = "f();";

            var runner = new ScriptCodeRunner(code, container);
            await runner.RunAsync();

            Assert.AreEqual(1, cnt);
        }
    }
}