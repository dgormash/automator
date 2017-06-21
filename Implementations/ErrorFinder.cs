using System.Collections.Generic;
using System.IO;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class ErrorFinder:IErrorFinder
    {
        public IEnumerable<string> FindErrors(string path)
        {
            return Directory.GetFiles(path, @"*.txt");
        }
    }
}
