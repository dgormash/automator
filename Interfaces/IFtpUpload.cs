namespace AutomatorPrg.Interfaces
{
    public interface IFtpUpload
    {
        string ErrorMessage { get; set; }
        void UploadFile(string file, string ftpPath, string login, string password);
    }
}