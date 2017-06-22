using System.Collections.Generic;
using System.IO;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class ErrorRemover:IErrorRemover
    {
        public void RemoveErrors(IEnumerable<string> files)
        {
            //string[] initialFile;
            //string[] fileWithErrors;
            //string[] finalFile;

            foreach (var file in files)
            {
                //todo Получить список строк исходного файла
                var initialFile = File.ReadAllLines(Path.GetFileNameWithoutExtension(file));
                //todo Получить список строк файла с ошибками
                var fileWithErrors = File.ReadAllLines(file);
                //todo Получить из списка ошибок номер строки с ошибкой
                //todo Получить из списка ошибок ошибочные атрибуты
                //
            }
        }

        private string[] GetFileContent(string file)
        {
            return File.ReadAllLines(file);
        }


    }
}