using System;
using AutomatorPrg.Implementations;

namespace AutomatorPrg.Interfaces
{
    public interface IFtpGetFileInfo
    {
        string ErrorMessage { get; set; }
        string Name { get; set; }
        DateTime LastModified { get; set; }
        FtpCommandStatus GetFileInfo(string ftpPath, string login, string password);
    }


}