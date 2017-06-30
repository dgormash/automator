namespace AutomatorPrg.Interfaces
{
    public interface IFtpGetDirectories
    {
        string ErrorMessage { get; set; }
        void GetDirectoryList (string ftpPath, string login, string password);
    }
}