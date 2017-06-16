using System.IO;
using System.Text;

namespace AutomatorPrg
{
    class FileAppender
    {
        public void AppendFile(string curFile, string addFile)
        {
            /*Если файл существует, то читаем его содержимое и... */
            using (var reader = new StreamReader(addFile, Encoding.GetEncoding(866)))
            {
                using (StreamWriter writer = new StreamWriter(curFile, true, Encoding.GetEncoding(866)))
                {
                    string someLine;
                    while ((someLine = reader.ReadLine()) != null)
                    {
                        /*... параллельно добавляем в готовый файл*/
                        writer.WriteLine(someLine);
                    }
                }
            }
            File.Delete(addFile);
        }
    }
}
