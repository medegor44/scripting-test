using System;

namespace CSharpScriptingTest
{
    public class Globals
    {
        public Func<string, string> Print = (str) =>
        {
            Console.WriteLine(str);
            return str + "$";
        };
    }

}
