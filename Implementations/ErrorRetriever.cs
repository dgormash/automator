using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    class ErrorRetriever:IErrorRetriever
    {
        public Dictionary<string, string> GetAllErrors(string file)
        {
            var result = new Dictionary<string, string>();
            var lines = File.ReadAllLines(file);
            var lineNum = string.Empty;
            var atributes = string.Empty;
            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    foreach (Match m in Regex.Matches(line, @"(^[0-9]+?):"))
                    {
                        lineNum = m.Groups[1].Value;
                    }

                    foreach (Match m in Regex.Matches(line, @"/([0-9]{2}(.+?))$"))
                    {
                        atributes = m.Groups[1].Value;
                    }

                    result.Add(lineNum, atributes);
                }
            }
            return result;
        }
    }
}
