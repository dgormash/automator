using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    internal class FtpCurrentDirectoryRetriever : IFtpGetCurrentDirectory
    {
        public string GetCurrentDirectory(IEnumerable<string> list)
        {
            var rDirectories = (from line in list from Match match in Regex.Matches(line, @"R[0-9]") select match.Groups[0].Value).ToList();
            rDirectories.Sort();
            var result = rDirectories.Last();
            return result;
        }
    }
}