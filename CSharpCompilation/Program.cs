using System.IO;
using System.Threading.Tasks;

namespace CSHarpCodeCompilationTest
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            using var sr = new StreamReader("input.txt");
            var script = await sr.ReadToEndAsync();

            var inMemoryCompilation = new CSharpCodeCompilation(script);
            inMemoryCompilation.Run();
        }
    }
}
