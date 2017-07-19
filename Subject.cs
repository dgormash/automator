using System.Collections.Generic;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg
{
    public class Subject:ISubject
    {
        private readonly List<IObserver> _reporters;
        private string _reportMessage;

        public Subject()
        {
            _reporters = new List<IObserver>();
        }
        public void Register(IObserver observer)
        {
            _reporters.Add(observer);
        }

        public void Unregister(IObserver observer)
        {
            _reporters.RemoveAt(_reporters.IndexOf(observer));
        }

        public void NotifyObserver()
        {
            foreach (var reporter in _reporters)
            {
                reporter.Update(_reportMessage);
            }
        }

        public void SetUpMessage(string message)
        {
            _reportMessage = message;
            NotifyObserver();
        }
    }
}