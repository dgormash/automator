namespace AutomatorPrg.Interfaces
{
    public interface IFtpCheckUploadingStatus
    {
        string ErrorMessage { get; set; }
        string FileName { get; set; }
        long FileSize { get; set; }
        FtpCommandStatus CheckUploadingStatus(string file, string ftpPath, string login, string password);
    }
}