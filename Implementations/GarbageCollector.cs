using System.IO;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class GarbageCollector:IGarbageCollector
    {

        public string ErrorDirectory { get; set; }
        public string TrashDirectory { get; set; }

        public void CleanUp(string path)
        {
            var txtFiles = Directory.GetFiles(path, "*.txt");

            foreach (var file in txtFiles)
            {
                var txtFileName = Path.GetFileName(file);
                if (File.Exists(string.Format(@"{0}\{1}", TrashDirectory, txtFileName)))
                {
                    File.Delete(string.Format(@"{0}\{1}", TrashDirectory, txtFileName));
                }
                File.Move(file, string.Format(@"{0}\{1}", TrashDirectory, txtFileName));
            }

            var errFiles = Directory.GetFiles(path, "*.err");

            foreach (var file in errFiles)
            {
                var errFileName = Path.GetFileName(file);
                if (File.Exists(string.Format(@"{0}\{1}", ErrorDirectory, errFileName)))
                {
                    File.Delete(string.Format(@"{0}\{1}", ErrorDirectory, errFileName));
                }
                File.Move(file, string.Format(@"{0}\{1}", ErrorDirectory, errFileName));
            }

            var oldFiles = Directory.GetFiles(path, "*.old");

            foreach (var file in oldFiles)
            {
                var oldFileName = Path.GetFileName(file);
                if (File.Exists(string.Format(@"{0}\{1}", TrashDirectory, oldFileName)))
                {
                    File.Delete(string.Format(@"{0}\{1}", TrashDirectory, oldFileName));
                }
                File.Move(file, string.Format(@"{0}\{1}", ErrorDirectory, oldFileName));
            }
        }
    }
}