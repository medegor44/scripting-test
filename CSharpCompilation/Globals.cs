using System;

namespace GlobalStuff
{
    public class Globals
    {
        public Func<string, string> Print = (text) =>
        {
            Console.WriteLine(text);
            return text + "$";
        };
    }
}