using System.Collections.Generic;

namespace AutomatorPrg.Interfaces
{
    public interface IArchiveFinder
    { 
        IEnumerable<string> FindArchives(string path);
    }
}