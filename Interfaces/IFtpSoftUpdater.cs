using System.Collections.Generic;
using System.IO;

namespace AutomatorPrg.Interfaces
{
    public interface IFtpSoftUpdater
    {
        string ErrorMessage { get; set; }
        bool CheckUpdatesResult { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        FtpCommandStatus GetFtpFileInfo(string ftpFile);
        FtpCommandStatus UpdateFile();

    }
}