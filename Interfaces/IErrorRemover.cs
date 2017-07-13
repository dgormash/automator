using System.Collections.Generic;

namespace AutomatorPrg.Interfaces
{
    public interface IErrorRemover
    {
        void RemoveErrors(IEnumerable<string> files, string searchPath);
    }
}