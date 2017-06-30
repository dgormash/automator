namespace AutomatorPrg.Interfaces
{
    public interface IFtpGetCurrentDirectoy
    {
        string ErrorMessage { get; set; }
        void GetCurrentDirectory (string ftpPath, string login, string password);
    }
}