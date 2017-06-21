using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class ErrorRemoverCreator:IErrorRemoverCreator
    {
        public IErrorRemover Create()
        {
            return new ErrorRemover();
        }
    }
}