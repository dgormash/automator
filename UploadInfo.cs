﻿using System.Collections.Generic;

namespace AutomatorPrg
{
    public class UploadInfo
    {
        public ushort Count { get; set; }
        public List<UploadedFileInfo> FileList { get; set; } 
        //todo доработать этот класс
    }
}