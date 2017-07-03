namespace AutomatorPrg.Interfaces
{
    public interface IFtpCheckUploadingStatus
    {
        string ErrorMessage { get; set; }
        FtpFileUploadingStatus CheckUploadingStatus(string file, string ftpPath, string login, string password);
    }
}