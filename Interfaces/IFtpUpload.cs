namespace AutomatorPrg.Interfaces
{
    public interface IFtpUpload
    {
        string ErrorMessage { get; set; }
        FtpCommandStatus UploadFile(string file, string ftpPath, string login, string password);
    }
}