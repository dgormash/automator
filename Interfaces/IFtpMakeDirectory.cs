namespace AutomatorPrg.Interfaces
{
    public interface IFtpMakeDirectory
    {
        string ErrorMessage { get; set; }
        FtpCommandStatus CreateDirectoryOnServer (string ftpPath, string login, string password);
    }
}