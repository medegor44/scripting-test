using System;

namespace GlobalStuff
{
    public class Globals
    {
        public static Func<string, string> Print = (str) =>
        {
            Console.WriteLine(str);
            return str + "$";
        };
    }
}
