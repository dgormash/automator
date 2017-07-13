using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class XmlReporter:IReporter
    {
        public XmlReporter(string file)
        {
            CreateFile(file);
        }
        public void WriteMessage(string message)
        {
            throw new System.NotImplementedException();
        }

        private static void CreateFile(string file)
        {
            
        }
    }
}