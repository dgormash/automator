using System.Collections;
using System.Collections.Generic;

namespace AutomatorPrg.Interfaces
{
    public interface IListReturner
    {
        IEnumerable<string> GetFileList(string path, string mask);
    }
}