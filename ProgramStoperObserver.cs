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
            Console.WriteLine(message);
            Environment.Exit(0);
        }
    }
}