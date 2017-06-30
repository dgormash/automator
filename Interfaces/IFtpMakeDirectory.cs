namespace AutomatorPrg.Interfaces
{
    public interface IFtpMakeDirectory
    {
        string ErrorMessage { get; set; }
        void CreateDirectoryOnServer (string ftpPath, string login, string password);
    }
}