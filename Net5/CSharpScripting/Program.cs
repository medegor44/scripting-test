using System.IO;
using System.Threading.Tasks;
using ScriptingLibrary;

namespace CSharpScriptingTest
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            using var sr = new StreamReader("input.txt");
            var script = await sr.ReadToEndAsync();

            var scriptRunner = new ScriptCodeRunner<Globals>(script);

            await scriptRunner.RunAsync();
        }
    }
}
