﻿using System;
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
            Console.WriteLine(message);
        }
    }
}