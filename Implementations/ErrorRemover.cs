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
        public void RemoveErrors(IEnumerable<string> files)
        {
            var i = 0;
            foreach (var file in files)
            {
                var errors = _errorRetriever.GetAllErrors(file);
                var curFile = Path.GetFileNameWithoutExtension(file); //имя оригинального файла
                File.Move(curFile, curFile + ".old");
                using (var reader = new StreamReader(curFile, Encoding.GetEncoding(866)))
                {
                    using (var newWriter = new StreamWriter(curFile, true, Encoding.GetEncoding(866)))
                    {
                        using (var errWriter = new StreamWriter(curFile + ".err", true, Encoding.GetEncoding(866)))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (errors.ContainsKey(i.ToString()))
                                {
                                    errWriter.WriteLine("{0};{1}", errors[i.ToString()], line);
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