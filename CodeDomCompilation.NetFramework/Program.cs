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
            var param = new CompilerParameters(
                new[] 
                { 
                    typeof(object).Assembly.Location, 
                    typeof(GlobalStuff.Globals).Assembly.Location 
                }, 
                "a.exe", 
                true);

            param.GenerateExecutable = false;

            string code = "";
            using (var sr = new StreamReader("input.txt"))
                code = sr.ReadToEnd();


            var res = provider.CompileAssemblyFromSource(param, code);

            if (res.Errors.Count > 0)
                res.Errors.Cast<CompilerError>().ToList().ForEach(error => Console.WriteLine(error.ErrorText));
            else
            {
                var assembly = res.CompiledAssembly;
                var type = assembly.GetType("Scripts.UserCode");

                var instance = Activator.CreateInstance(type);

                type.InvokeMember(
                    "Main", 
                    System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.InvokeMethod,
                    null, 
                    instance,
                    new object[] { "Hello world" }
                    );
            }
        }
    }
}
