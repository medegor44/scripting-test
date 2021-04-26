using System;
using System.IO;
using System.Threading.Tasks;

namespace CSharpScriptingTest
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            using var sr = new StreamReader("input.txt");
            var script = await sr.ReadToEndAsync();

            var scriptRunner = new CSharpScripting();

            var result = await scriptRunner.RunScriptAsync<string>(script, new Globals());

            Console.WriteLine(result);
        }
    }
}
