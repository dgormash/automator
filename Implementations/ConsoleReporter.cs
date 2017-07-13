using System;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class ConsoleReporter:IReporter
    {
        public ConsoleColor Color { set; private get; }
        public void WriteMessage(string message)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}