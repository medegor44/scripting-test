using System.Threading.Tasks;
using Jint;

namespace ScriptingLibrary
{
    public class JintJsCodeRunner : ICodeRunner
    {
        public JintJsCodeRunner(string code, Container container)
        {
            Code = code;
            Container = container;
        }

        public async Task RunAsync()
        {
            var engine = new Engine();

            foreach (var entry in Container.Names)
            {
                var name = entry.Value;
                var type = entry.Key;

                engine = engine.SetValue(name, Container.Resolve(type));
            }

            await Task.Run(() => engine.Execute(Code));
        }

        private string Code { get; set; }
        private Container Container { get; set; }
    }
}