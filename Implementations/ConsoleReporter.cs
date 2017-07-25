using System;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class ConsoleReporter: IObserver
    {
        public ConsoleReporter(ISubject subject)
        {
            subject.Register(this);
        }

        public void Update(string message)
        {
            if (message.StartsWith(@"\r"))
                Console.ForegroundColor = ConsoleColor.Red;
            if(message.StartsWith(@"\y"))
                Console.ForegroundColor = ConsoleColor.Yellow;
            if(message.StartsWith(@"\g"))
                Console.ForegroundColor = ConsoleColor.Green;
            if (message.StartsWith(@"\b"))
                Console.ForegroundColor = ConsoleColor.Cyan;
            if(message.StartsWith(@"\w"))
                Console.ForegroundColor = ConsoleColor.White;
            if (message.StartsWith(@"\m"))
                Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message.Remove(0,2));
            Console.ResetColor();
        }
    }
}