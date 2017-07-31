using System;
using System.IO;
using System.Reflection;
using System.Text;
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
            File.AppendAllText($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\report\{
                DateTime.Now.ToString("ddMMyyyy")}.rep", $"{DateTime.Now.ToLocalTime()}: {message.Remove(0, 2)}{Environment.NewLine}", Encoding.GetEncoding(1251));
        }
    }
}