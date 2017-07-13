using System.Diagnostics;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class CommonChecker:IChecker
    {
        /// <summary>
        /// Общий чеккер, для запуска без параметров. Если после проверки не обнаружатся ошибки, то создаётся
        /// архив, соответствующий типу входящего файла. Если будут ошибки, создатся файл исходное_имя_файла.номер.txt
        /// Свойствой CheckerLocation указывает на путь к файлу чеккера.
        /// Метод StartChecking(string file) запускает провеку текущего файла.
        /// </summary>
        public string CheckerLocation { get; set; }
        public void StartChecking(string file)
        {
            var process = new Process
            {
                StartInfo =
                    {
                        FileName = $@"""{CheckerLocation}""",
                        Arguments = $@" -o -ne ""{file}"""
                    }
            };
            process.StartInfo.UseShellExecute = true;
            process.Start();
            process.WaitForExit();
        }
    }
}