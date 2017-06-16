using System;
using System.Diagnostics;
using System.IO;

namespace AutomatorPrg
{
    class CheckStarter
    {
        //private string _file;

        public void StartChekcer(string file)
        {
            ConsoleMessage cMsg = new ConsoleMessage();
            try
            {
                cMsg.WriteLine(@"Запускаю  проверку файлом chkNewArv.exe...", @"Message");
                Process test = new Process
                {
                    StartInfo =
                    {
                        FileName = string.Format(@"""{0}\misc\chkNewArv.exe""",Directory.GetCurrentDirectory()),
                        Arguments = string.Format(@" -o -ne ""{0}""", file)
                    }
                };
                test.StartInfo.UseShellExecute = false;
                test.Start();
                test.WaitForExit();
            }
            catch (Exception err)
            {
                cMsg.WriteLine(string.Format(@"{0}: {1}\misc\chkNewArv.exe", err.Message, Directory.GetCurrentDirectory()),
                    @"Error");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
