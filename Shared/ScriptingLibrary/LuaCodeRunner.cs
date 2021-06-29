using System;
using System.Threading.Tasks;
using MoonSharp.Interpreter;

namespace ScriptingLibrary
{
    public class LuaCodeRunner : ICodeRunner
    {
        public LuaCodeRunner(string code, Container container)
        {
            Code = code;
            Container = container;
        }

        public async Task RunAsync()
        {
            var script = new Script();

            foreach (var entry in Container.Names)
            {
                var type = entry.Key;
                var name = entry.Value;

                
                if (type.IsPrimitive || type == typeof(Action<>))
                    script.Globals[name] = Container.Resolve(type);
                else
                {
                    UserData.RegisterType(type);
                    var scriptObj = UserData.Create(Container.Resolve(type));

                    script.Globals.Set(name, scriptObj);
                }
            }
            await Task.Run(() => script.DoString(Code));
        }

        private string Code { get; set; }
        private Container Container { get; set; }
    }
}