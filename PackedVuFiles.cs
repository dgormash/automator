using System.Diagnostics;
using System.IO;
using AutomatorPrg.Abstractions;

namespace AutomatorPrg
{
    public class PackedVuFiles
    {
        public override void CreateList(string path)
        {
            _list = Directory.GetFiles(path, @"V75A6FD.???");
        }

        //public override void CheckFile()
        //{
            


        //    }
        //}
    }
}