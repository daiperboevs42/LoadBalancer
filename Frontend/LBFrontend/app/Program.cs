using System;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            Printer printer = new Printer();
            new Requestmaker();
            printer.Options();
        }
    }
}
