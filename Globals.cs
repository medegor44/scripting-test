using System;

namespace scripting_test
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
