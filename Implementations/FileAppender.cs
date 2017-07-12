using System.IO;
using System.Text;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FileAppender : IFileAppender
    {
        public void AppendFile(string outgoingFile, string additiveFile)
        {
            /*Если файл существует, то читаем его содержимое и... */

            if (!File.Exists(additiveFile)) return;
            using (var reader = new StreamReader(additiveFile, Encoding.GetEncoding(866)))
            {
                using (var writer = new StreamWriter(outgoingFile, true, Encoding.GetEncoding(866)))
                {
                    string additiveString;
                    while ((additiveString = reader.ReadLine()) != null)
                    {
                        /*... параллельно добавляем в готовый файл*/
                        writer.WriteLine(additiveString);
                    }
                }
            }
            File.Delete(additiveFile);
        }
    }
}