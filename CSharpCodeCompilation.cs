using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace scripting_test
{
    class CSharpCodeCompilation
    {
        public CSharpCodeCompilation(string code)
        {
            Compile(code);
        }

        public void Run()
        {
            using var memoryStream = new MemoryStream();
            var result = Compilation.Emit(memoryStream);

            if (!result.Success)
            {
                var failures = result.Diagnostics
                .Where(d => d.IsWarningAsError || d.Severity == DiagnosticSeverity.Error);

                foreach (var diagnostic in failures)
                    Console.Error.WriteLine($"{diagnostic.Id}: {diagnostic.GetMessage()}");
            }
            else
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                var assembly = Assembly.Load(memoryStream.ToArray());

                var type = assembly.GetType("Script.Writer");
                var instance = Activator.CreateInstance(type);

                type.InvokeMember("Write",
                BindingFlags.Default | BindingFlags.InvokeMethod,
                null,
                instance,
                new[] { "Hello world" });
            }
        }

        private CSharpCompilation Compilation { get; set; }

        private void Compile(string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);

            var assemblyName = Path.GetRandomFileName();

            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
            };

            Compilation = CSharpCompilation.Create(
                assemblyName,
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );
        }
    }
}