using System;
using System.IO;
using System.Reflection;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class LogReporter:IObserver
    {
        public LogReporter(ISubject subject)
        {
            subject.Register(this);
        }
        public void Update(string message)
        {
            using (var writer = File.AppendText(
                $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\report\{
                    DateTime.Now.ToString("ddMMyyyy")}.rep"))
            {
                writer.WriteLine(message.Remove(0,2));
            }
        }
    }
}