using System.Collections.Generic;

namespace AutomatorPrg
{
    public class FileDstributionInfo
    {
        public ushort Count { get; set; }
        public List<UploadedFileInfo> FileList { get; set; } 
        //todo доработать этот класс
    }
}