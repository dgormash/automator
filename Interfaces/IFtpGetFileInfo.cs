using AutomatorPrg.Implementations;

namespace AutomatorPrg.Interfaces
{
    public interface IFtpGetFileInfo
    {
        string ErrorMessage { get; set; }
        FtpFileInfo Result { get; set; }
        FtpCommandStatus GetFileInfo(string ftpPath, string login, string password);
    }


}