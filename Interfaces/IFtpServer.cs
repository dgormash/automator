using Microsoft.SqlServer.Server;

namespace AutomatorPrg.Interfaces
{
    public interface IFtpServer
    {
        void SetUploadMethod(IFtpUpload uploadMethod);
        void SetDownloadMethod(IFtpDownload downloadMethod);
        void SetCheckingMethod(IFtpCheckUploadingStatus checkingMethod);
        void SetGetDirectoriesMethod(IFtpGetDirectories getDirectoriesMethod);
        void SetMakeDirectoryMethod(IFtpMakeDirectory makeDirectoryMethod);
        void SetGetCurrentDirectoryMethod(IFtpGetCurrentDirectory getCurrentDirectoryMethod);
        //void SetLogin(IFtpSetLogin setLoginMethod);
        //void SetPassword(IFtpSetPassword setPasswordMethod);
        //void SetAddress(IFtpSetAddress setAddressMethod);
        void SetLogin(string value);
        void SetPassword(string value);
        void SetAddress(string value);
    }


}