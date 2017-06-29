using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpServer:IFtpServer
    {
        private string _login;
        private string _password;
        private string _address;
        private IFtpUpload _uploader;
        public void SetUploadMethod(IFtpUpload uploadMethod)
        {
            _uploader = uploadMethod;    
        }

        public void SetDownloadMethod(IFtpDownload downloadMethod)
        {
            throw new System.NotImplementedException();
        }

        public void SetCheckingMethod(IFtpCheckUpoladingStatus checkingMethod)
        {
            throw new System.NotImplementedException();
        }

        public void SetGetDirectoriesMethod(IFtpGetDirectories getDirectoriesMethod)
        {
            throw new System.NotImplementedException();
        }

        public void SetMakeDirectoryMethod(IFtpMakeDirectory makeDirectoryMethod)
        {
            throw new System.NotImplementedException();
        }

        public void SetGetCurrentDirectoryMethod(IFtpGetCurrentDirectoy getCurrentDirectoryMethod)
        {
            throw new System.NotImplementedException();
        }

        public void SetLogin(string login)
        {
            throw new System.NotImplementedException();
        }

        public void SetPassword(string password)
        {
            throw new System.NotImplementedException();
        }

        public void SetAddress(string address)
        {
            throw new System.NotImplementedException();
        }
    }
}