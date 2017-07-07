namespace AutomatorPrg.Interfaces
{
    public interface IFtpCheckUploadingStatus
    {
        string ErrorMessage { get; set; }
        UploadedFileInfo FtpFileInfo { get; set; }
        FtpCommandStatus CheckUploadingStatus(string file, string ftpPath, string login, string password);
    }
}