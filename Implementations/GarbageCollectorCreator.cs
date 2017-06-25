using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class GarbageCollectorCreator:IGarbageCollectorCreator
    {
        public IGarbageCollector Create()
        {
            return  new GarbageCollector();
        }
    }
}