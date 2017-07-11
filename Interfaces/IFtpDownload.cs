namespace AutomatorPrg.Interfaces
{
    public interface IFtpDownload
    {
        string ErrorMessage { get; set; }
        FtpCommandStatus DownloadFile (string what, string where, string login, string password);
    }
}