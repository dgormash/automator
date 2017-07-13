using System.Collections.Generic;
using System.IO;
using System.Text;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class ErrorRemover:IErrorRemover
    {
        private readonly IErrorRetriever _errorRetriever;

        public ErrorRemover(IErrorRetriever retriever)
        {
            _errorRetriever = retriever;
        }
        public void RemoveErrors(IEnumerable<string> files, string searchPath)
        {
            var i = 1;
            foreach (var file in files)
            {
                var errors = _errorRetriever.GetAllErrors(file);
                var curFile = $@"{searchPath}\{Path.GetFileNameWithoutExtension(file)}"; //имя оригинального файла
                File.Move(curFile, curFile + ".old");
                using (var reader = new StreamReader(curFile + ".old", Encoding.GetEncoding(866)))
                {
                    using (var newWriter = new StreamWriter(curFile, true, Encoding.GetEncoding(866)))
                    {
                        using (var errWriter = new StreamWriter(curFile + ".err", true, Encoding.GetEncoding(866)))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (errors.ContainsKey(i.ToString("D7")))
                                {
                                    errWriter.WriteLine("{0};{1}", errors[i.ToString("D7")], line);
                                }
                                else
                                {
                                    newWriter.WriteLine(line);
                                }
                                ++i;
                            }
                        }
                    }
                }
            }
        }
    }
}