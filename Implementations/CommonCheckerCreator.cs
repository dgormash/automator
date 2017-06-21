using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class CommonCheckerCreator:ICheckerCreator
    {
        public IChecker CreateChecker()
        {
            return new CommonChecker();
        }
    }
}