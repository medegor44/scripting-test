using System.Threading.Tasks;
using JavaScriptEngineSwitcher.Jint;

namespace ScriptingLibrary
{
    public class JsCodeRunner : ICodeRunner
    {
        public JsCodeRunner(string code, Container container)
        {
            Code = code;
            Container = container;
        }

        public async Task RunAsync()
        {
            using var engine = new JintJsEngine();

            foreach (var entry in Container.Names)
            {
                var name = entry.Value;
                var type = entry.Key;
                engine.SetVariableValue(name, Container.Resolve(type));
            }

            await Task.Run(() => engine.Execute(Code));
        }

        public string Code { get; set; }
        private Container Container { get; set; }
    }
}