using System.IO;
using System.Threading.Tasks;
using ScriptingLibrary;

namespace CSHarpCodeCompilationTest
{
    static class Program
    {
        static async Task Main()
        {
            using var sr = new StreamReader("input.txt");
            var script = await sr.ReadToEndAsync();

            var runner = new CompiledCodeRunner<GlobalStuff.Globals>(script);
            await runner.RunAsync();
        }
    }
}
