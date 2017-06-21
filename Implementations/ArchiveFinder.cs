using System.Collections.Generic;
using System.IO;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class ArchiveFinder:IArchiveFinder
    {
        public IEnumerable<string> FindArchives(string path)
        {
            return Directory.GetFiles(path, @"?75????.rar");
        }
    }
}