using AutomatorPrg.Interfaces;

namespace AutomatorPrg
{
    public class Reporter

    {
        private readonly IReporter _reporter;

        public Reporter(IReporter reporter)
        {
            _reporter = reporter;
        }

        public void WriteMessage(string message)
        {
            _reporter.WriteMessage(message);
        }
    }
}