using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    internal class ProgramStoperSubject:ISubject
    {
        private readonly List<IObserver> _stoper;
        public ProgramStoperSubject()
        {
            _stoper = new List<IObserver>();
        }
       

        public void Register(IObserver observer)
        {
            _stoper.Add(observer);
        }

        public void Unregister(IObserver observer)
        {
            _stoper.RemoveAt(_stoper.IndexOf(observer));
        }

        public void NotifyObserver()
        {
            foreach (var stopCase in _stoper)
            {
                stopCase.Update("В результате работы программы возникла ошибка. Программа будет закрыта");
            }
        }

        public void StopProgramByEmergency()
        {
            NotifyObserver();
        }
    }
}