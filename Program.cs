using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace scripting_test
{
    public class Globals
    {
        public Func<string, string> Print = (str) =>
        {
            Console.WriteLine(str);
            return str + "$";
        };
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            using (var sr = new StreamReader("input.txt"))
            {
                var script = sr.ReadToEnd();

                var res = await CSharpScript.RunAsync(script, globals: new Globals());
            }
        }
    }
}
