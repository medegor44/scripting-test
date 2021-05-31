using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ScriptingLibrary
{
    public class CodeCompiler
    {
        public CodeCompiler(string code, Container container)
        {
            Container = container;

            Compile(code);
        }

        public CSharpCompilation Compilation { get; set; }

        private void Compile(string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);

            var assemblyName = Path.GetRandomFileName();

            var references = GenerateMetadataReferences();

            Compilation = CSharpCompilation.Create(
                assemblyName,
                new[] {syntaxTree},
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );
        }

        private List<MetadataReference> GenerateMetadataReferences()
        {
            var refs = new List<MetadataReference>();

            foreach (var type in Container.Instances.Keys)
            {
                var reference = MetadataReference.CreateFromFile(type.Assembly.Location);

                refs.Add(reference);
            }

            var dotNetCoreDir = Path.GetDirectoryName(typeof(object).Assembly.Location);

            refs.AddRange(new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "System.Runtime.dll")),
                MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDir, "netstandard.dll")),
                MetadataReference.CreateFromFile(typeof(Container).Assembly.Location),
            });

            return refs;
        }

        private Container Container { get; set; }
    }
}
