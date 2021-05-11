using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;

namespace CodeDomCompilation
{
    class Program
    {
        static void Main()
        {
            var provider = new CSharpCodeProvider();
            var param = new CompilerParameters(new[] { typeof(object).Assembly.Location }, "a.exe", true);

            using var sr = new StreamReader("input.txt");
            var code = sr.ReadToEnd();

            var res = provider.CompileAssemblyFromSource(param, code);

            res.Errors.Cast<CompilerError>().ToList().ForEach(error => Console.WriteLine(error.ErrorText));
        }
    }
}
