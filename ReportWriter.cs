using System;
using System.IO;
using System.Text;

namespace AutomatorPrg
{
    class ReportWriter
    {
        private readonly string _reportFile;

        public ReportWriter(string name, string path)
        {
            _reportFile = string.Format(@"{0}\report\{1}.rep", path, name);

            if (!File.Exists(_reportFile))
            {
                try
                {
                    FileStream newFile = File.Create(_reportFile);
                    newFile.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(@"{0}.rep: {1}", name, e.Message);
                }
            }
        }

        public void WriteToFile(string msg)
        {
            using (
                    StreamWriter outFile =
                        new StreamWriter(
                            _reportFile,
                            true,
                            Encoding.GetEncoding(1251)))
            {
                outFile.WriteLine(msg);
            }
        }
    }
}
