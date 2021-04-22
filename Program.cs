using System;
using System.IO;
using System.Threading.Tasks;

namespace scripting_test
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            using var sr = new StreamReader("input.txt");
            var script = await sr.ReadToEndAsync();

            var scriptingEngine = new CSharpScripting();
            var result = await scriptingEngine.RunScriptAsync<string>(script, new Globals());

            Console.WriteLine(result);
        }
    }
}
