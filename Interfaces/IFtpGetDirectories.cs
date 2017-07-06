using System.Collections.Generic;

namespace AutomatorPrg.Interfaces
{
    public interface IFtpGetDirectories
    {
        string ErrorMessage { get; set; }
        List<string> GetDirectoryList (string ftpPath, string login, string password);
    }
}