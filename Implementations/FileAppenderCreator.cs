using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FileAppenderCreator:IFileAppenderCreator
    {
        public IFileAppender Create()
        {
            return new FileAppender();
        }
    }
}