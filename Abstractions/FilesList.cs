using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AutomatorPrg.Abstractions
{
    public  class FilesList
    {
        protected IEnumerable<string> _list;

        public bool ReCheckFile { get; private set; }

        public FilesList(string path, string mask)
        {
            _list = Directory.GetFiles(path, mask);
        }

        public virtual void ChekFiles()
        {
            foreach (var file in _list)
            {
                var checkApp = new Process
                {
                    StartInfo =
                    {
                        FileName = string.Format(@"""{0}\misc\chkNewArv.exe""",Directory.GetCurrentDirectory()),
                        Arguments = string.Format(@" -o -ne ""{0}""", file)
                    }
                };
                checkApp.StartInfo.UseShellExecute = false;
                checkApp.Start();
                checkApp.WaitForExit();
            }











    }

}