using System;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg
{
    internal class ProgramStoperObserver:IObserver
    {
        public ProgramStoperObserver(ISubject subject)
        {
            subject.Register(this);
        }
        public void Update(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
            //Console.ReadKey();
            Environment.Exit(0);
        }
    }
}