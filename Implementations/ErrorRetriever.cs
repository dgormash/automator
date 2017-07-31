using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    internal class ErrorRetriever:IErrorRetriever
    {
        public Dictionary<string, string> GetAllErrors(string file)
        {
            var result = new Dictionary<string, string>();
            var lines = File.ReadAllLines(file, Encoding.GetEncoding(866));
            var lineNum = string.Empty;
            var atributes = string.Empty;
            foreach (var line in lines.Where(line => line != "" && line != " "))
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
            return result;
        }
    }
}
