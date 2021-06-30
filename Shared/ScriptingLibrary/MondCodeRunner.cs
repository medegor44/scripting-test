using System;
using System.Linq;
using System.Threading.Tasks;
using Mond;

namespace ScriptingLibrary
{
    public class MondCodeRunner : ICodeRunner
    {
        public MondCodeRunner(string code, Container container)
        {
            Code = code;
            Container = container;
        }

        public async Task RunAsync()
        {
            var state = new MondState();
            
        }

        public MondCodeRunner RegisterFunction(Delegate f, string alias)
        {
            State[alias] = MondValue.Function((_, args) =>
            {
                f.DynamicInvoke(args.Select(val => val.UserData));
                return MondValue.Undefined;
            });

            return this;
        }

        private MondState State { get; set; } = new();
        private string Code { get; set; }
        private Container Container { get; set; }
    }
}