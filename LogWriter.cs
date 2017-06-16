using System;
using System.IO;
using System.Text;

namespace AutomatorPrg
{
    public class LogWriter
    {
        private readonly string _fileName;
        private readonly string _filePath;

        public LogWriter (string fName, string fPath)
            {
                _fileName = fName;
                _filePath = fPath;
                string logfile = string.Format(@"{0}\{1}.log", _filePath, _fileName);
                if (!File.Exists(logfile))
                {
                    try
                    {
                        FileStream newFile = File.Create(string.Format(@"{0}\{1}.log", _filePath, _fileName));
                        newFile.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(@"{0}.log: {1}", _fileName, e.Message);
                    } 
                }    
            }

            //Запись в файл
            public void WriteToFile(string msg)
            {
                string logFile = string.Format(@"{0}\{1}.log", _filePath, _fileName);
                using (
                        StreamWriter outFile =
                            new StreamWriter(
                                logFile,
                                true,
                                Encoding.GetEncoding(1251)))
                {
                    outFile.WriteLine(msg);
                }
            }

        }
    }

