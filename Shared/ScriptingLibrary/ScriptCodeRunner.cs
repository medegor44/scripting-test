using System.Collections.Immutable;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Scripting;

namespace ScriptingLibrary
{
    public class ScriptCodeRunner : ICodeRunner
    {
        public class Globals
        {
            public Container Container { get; set; }
        }

        public ScriptCodeRunner(string code, Container container)
        {
            Code = code;
            Container = container;

            Code = new ResolvingCodeGenerator(container).ResolveLines + Code;

            var optsCtor = typeof(ScriptOptions)
                .GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var opts = optsCtor[0].Invoke(new object[]
            {
                string.Empty,
                ImmutableArray<MetadataReference>.Empty,
                ImmutableArray<string>.Empty,
                ScriptMetadataResolver.Default,
                SourceFileResolver.Default,
                false,
                null,
                OptimizationLevel.Debug,
                false,
                true,
                4,
                null
            }) as ScriptOptions;

            Options = opts;

            AddImportsAndReferences();
        }

        public async Task RunAsync()
        {
            await CSharpScript.RunAsync(Code, globals: new Globals { Container = Container }, options: Options);
        }

        private void AddImportsAndReferences()
        {
            foreach (var type in Container.Instances.Keys)
            {
                Options = Options.AddImports(type.Namespace);
                Options = Options.WithReferences(type.Assembly);
            }
        }

        private ScriptOptions Options { get; set; }
        private Container Container { get; }
        private string Code { get; }
    }
}
/*
var runner = new ScriptCodeRunner();

runner.AddCode(code).AddParam<TPar1>(par1, name1).AddParam<TPar2>(par2, name2).Run();
 */