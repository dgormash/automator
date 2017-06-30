namespace AutomatorPrg.Interfaces
{
    public interface IFtpDownload
    {
        string ErrorMessage { get; set; }
        void DownloadFile (string file, string ftpPath, string login, string password);
    }
}