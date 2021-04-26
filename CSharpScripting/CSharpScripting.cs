using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace CSharpScriptingTest
{
    class CSharpScripting
    {
        public async Task<T> RunScriptAsync<T>(string script, Globals globals)
        {
            var res = await CSharpScript.RunAsync<T>(script, globals: globals);
            return res.ReturnValue;
        }
    }
}
