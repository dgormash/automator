using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class ErrorFinderCreator:IErrorFinderCreator
    {
        public IErrorFinder Create()
        {
            return new ErrorFinder();
        }
    }
}