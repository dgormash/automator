using System.Diagnostics;
using System.IO;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class CommonChecker:IChecker
    {
        public string CheckerLocation { get; set; }

       

        public void StartChecking(string file)
        {
            var process = new Process
            {
                StartInfo =
                    {
                        FileName = string.Format(@"""{0}""", CheckerLocation),
                        Arguments = string.Format(@" -o -ne ""{0}""", file)
                    }
            };
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
        }
    }
}