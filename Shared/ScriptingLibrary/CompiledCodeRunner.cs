using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ScriptingLibrary
{
    public class CompiledCodeRunner : ICodeRunner
    {
        public CompiledCodeRunner(string code, Container container)
        {
            var template =
@"
using {0};
{1}

public class Runner
{{
    public void Run(Container Container) 
    {{ 
       {2}
       {3}
    }} 
}}";

            var generator = new ResolvingCodeGenerator(container);

            var usingLines = generator.UsingLines;
            var resolveLines = generator.ResolveLines;

            var codeForCompilation = string.Format(
                template, 
                typeof(Container).Namespace,
                usingLines,
                resolveLines,
                code);

            Container = container;
            Compilation = new CodeCompiler(codeForCompilation, container).Compilation;
        }

        public async Task RunAsync()
        {
            using var memoryStream = new MemoryStream();
            var result = Compilation.Emit(memoryStream);

            if (!result.Success)
            {
                var failures = result.Diagnostics
                .Where(d => d.IsWarningAsError || d.Severity == DiagnosticSeverity.Error)
                .Select(diagnostic => $"{diagnostic.Id}: {diagnostic.GetMessage()}");

                throw new Exception(string.Join(Environment.NewLine, failures));
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            var assembly = Assembly.Load(memoryStream.ToArray());

            var type = assembly.GetType("Runner");
            var instance = Activator.CreateInstance(type);

            type.InvokeMember("Run",
                BindingFlags.Default | BindingFlags.InvokeMethod,
                null,
                instance, 
                new object[] { Container }
            );
        }

        private CSharpCompilation Compilation { get; }
        private Container Container { get; }
    }
}
