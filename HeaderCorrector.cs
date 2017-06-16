using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace errRemover
{
    class HeaderCorrector
    {
        private List<string> _hElements = new List<string>();
        private string _newHeader;
        
        public string CorrectHeader(string header)
        {
            //_hElements = Regex.Split(header, @"\|(.+?)\|", RegexOptions.IgnoreCase);
            //_hElements[0].Remove(1);
            foreach (Match m in Regex.Matches(header, @"\|\b(.+?)\b\|"))
            {
                _hElements.Add(m.Groups[0].Value);
            }
            return _newHeader;
        }
    }
}
