using System.IO;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class GarbageCollector:IGarbageCollector
    {

        public string MoveTo { set; get; }

        public void CleanUp(string path, string mask)
        {
            var fileList = Directory.GetFiles(path, mask);

            foreach (var file in fileList)
            {
                var fileName = Path.GetFileName(file);
                if (File.Exists($@"{MoveTo}\{fileName}"))
                {
                    File.Delete($@"{MoveTo}\{fileName}");
                }
                File.Move(file, $@"{MoveTo}\{fileName}");
            }
        }
    }
}