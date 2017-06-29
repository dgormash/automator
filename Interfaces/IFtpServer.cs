using System.Reflection.Emit;

namespace AutomatorPrg.Interfaces
{
    public interface IFtpServer
    {
        void SetUploadMethod(IFtpUpload uploadMethod);
        void SetDownloadMethod(IFtpDownload downloadMethod);
        void SetCheckingMethod(IFtpCheckUpoladingStatus checkingMethod);
        void SetGetDirectoriesMethod(IFtpGetDirectories getDirectoriesMethod);
        void SetMakeDirectoryMethod(IFtpMakeDirectory makeDirectoryMethod);
        void SetGetCurrentDirectoryMethod(IFtpGetCurrentDirectoy getCurrentDirectoryMethod);
        void SetLogin(string login);
        void SetPassword(string password);
        void SetAddress(string address);
    }
}