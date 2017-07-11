using System.IO;

namespace AutomatorPrg.Interfaces
{
    public interface ISoftUpdateCheck
    {//todo Удалить интерфейс
        string Login { get; set; }
        string Password { get; set; }
        bool IsFtpFileNewerThanLocalFile(FileInfo localFile, string ftpFile);
    }
}