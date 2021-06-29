using System.Threading.Tasks;
using NiL.JS.Core;

namespace ScriptingLibrary
{
    public class NilJsCodeRunner : ICodeRunner
    {
        public NilJsCodeRunner(string code, Container container)
        {
            Code = code;
            Container = container;
        }

        public async Task RunAsync()
        {
            var context = new Context();

            foreach (var entry in Container.Names)
            {
                var name = entry.Value;
                var type = entry.Key;

                context.DefineVariable(name).Assign(JSValue.Marshal(Container.Resolve(type)));
            }

            await Task.Run(() => context.Eval(Code));
        }

        private string Code { get; set; }
        private Container Container { get; set; }
    }
}