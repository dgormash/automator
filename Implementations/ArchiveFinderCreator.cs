using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class ArchiveFinderCreator:IArchiveFinderCreator
    {
        public IArchiveFinder Create()
        {
            return new ArchiveFinder();
        }
    }
}