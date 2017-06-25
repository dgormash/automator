using System.Collections.Generic;

namespace AutomatorPrg.Interfaces
{
    public interface IErrorRetriever
    {
        Dictionary<string, string> GetAllErrors(string file);
    }
}