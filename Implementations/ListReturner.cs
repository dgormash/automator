using System.Collections.Generic;
using System.IO;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class ListReturner:IListReturner
    {
        public IEnumerable<string> GetFileList(string path, string mask)
        {
           return Directory.GetFiles(path, mask);
        }
    }
}