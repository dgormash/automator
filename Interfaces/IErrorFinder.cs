using System.Collections.Generic;

namespace AutomatorPrg.Interfaces
{
    public interface IErrorFinder
    {
        IEnumerable<string> FindErrors(string path);
    }
}