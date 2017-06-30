using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpServer:IFtpServer
    {
        private string _login;
        private string _password;
        private string _address;
        private IFtpUpload _uploader;
        private IFtpDownload _download;
        private IFtpCheckUploadingStatus _checkResult;
        private IFtpGetDirectories _directories;
        private IFtpMakeDirectory _directoryMaker;
        private IFtpGetCurrentDirectory _currnetDirectory;
        public void SetUploadMethod(IFtpUpload uploadMethod)
        {
            _uploader = uploadMethod;    
        }

        public void SetDownloadMethod(IFtpDownload downloadMethod)
        {
            _download = downloadMethod;
        }

        public void SetCheckingMethod(IFtpCheckUpoladingStatus checkingMethod)
        {
            _checkResult = checkingMethod;
        }

        public void SetGetDirectoriesMethod(IFtpGetDirectories getDirectoriesMethod)
        {
            _directories = getDirectoriesMethod;
        }

        public void SetMakeDirectoryMethod(IFtpMakeDirectory makeDirectoryMethod)
        {
            _directoryMaker = makeDirectoryMethod;
        }

        public void SetGetCurrentDirectoryMethod(IFtpGetCurrentDirectoy getCurrentDirectoryMethod)
        {
            _currnetDirectory = getCurrentDirectoryMethod;
        }

        public void SetLogin(string login)
        {
            _login = login;
        }

        public void SetPassword(string password)
        {
            _password = password;
        }

        public void SetAddress(string address)
        {
            _address = address;
        }
    }
}