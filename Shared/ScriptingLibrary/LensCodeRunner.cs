using System.Threading.Tasks;
using Lens;

namespace ScriptingLibrary
{
    public class LensCodeRunner : ICodeRunner
    {
        public LensCodeRunner(string code, Container container)
        {
            Code = code;
            Container = container;
        }

        public async Task RunAsync()
        {
            var compiler = new LensCompiler();

            foreach (var entry in Container.Names)
            {
                var name = entry.Value;
                var type = entry.Key;

                compiler.RegisterProperty(name, () => Container.Resolve(type));
            } 
            var compiled = compiler.Compile(Code);
            await Task.Run(compiled);
        }

        private string Code { get; set; }
        private Container Container { get; set; }
    }
}