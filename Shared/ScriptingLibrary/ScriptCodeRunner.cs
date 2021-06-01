using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using System.Runtime;

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


        private ScriptOptions Options { get; set; } = ScriptOptions.Default;
        private Container Container { get; }
        private string Code { get; }
    }
}
/*
var runner = new ScriptCodeRunner();

runner.AddCode(code).AddParam<TPar1>(par1, name1).AddParam<TPar2>(par2, name2).Run();
 */