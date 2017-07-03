using System.Globalization;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpServer:IFtpServer
    {
        //todo Возможно понадобятся методы доступа к полям (операциям)
        //private IFtpSetLogin _login;
        //private IFtpSetPassword _password;
        //private IFtpSetAddress _address;

        private string _login;
        private string _password;
        private string _address;
        private IFtpUpload _uploader;
        private IFtpDownload _downloader;
        private IFtpCheckUploadingStatus _checkResult;
        private IFtpGetDirectories _directories;
        private IFtpMakeDirectory _directoryMaker;
        private IFtpGetCurrentDirectory _currnetDirectory;
        public void SetUploadMethod(IFtpUpload uploadMethod)
        {
            _uploader = uploadMethod;    
        }

        public void UploadFile(string file)
        {
            _uploader.UploadFile(file, _address, _login, _password);
        }

        public void SetDownloadMethod(IFtpDownload downloadMethod)
        {
            _downloader = downloadMethod;
        }

        public void DownloadFile()
        {
            
        }

        public void SetCheckingMethod(IFtpCheckUploadingStatus checkingMethod)
        {
            _checkResult = checkingMethod; 
        }

        public void CheckUploadingStatus()
        {
            
        }


        public void SetGetDirectoriesMethod(IFtpGetDirectories getDirectoriesMethod)
        {
            _directories = getDirectoriesMethod;
        }

        public void GetDirectoriesList()
        {
            
        }

        public void SetMakeDirectoryMethod(IFtpMakeDirectory makeDirectoryMethod)
        {
            _directoryMaker = makeDirectoryMethod;
        }

        public void MakeDirectoryOnFtpServer()
        {
            
        }

        public void SetGetCurrentDirectoryMethod(IFtpGetCurrentDirectory getCurrentDirectoryMethod)
        {
            _currnetDirectory = getCurrentDirectoryMethod;
        }

        public void GetCurrentDirectory()
        {
            
        }

        public void SetLogin(string value)
        {
            _login = value;
        }

        public void SetPassword(string value)
        {
            _password = value;
        }

        public void SetAddress(string value)
        {
            _address = value;
        }

        //public void SetLogin(IFtpSetLogin setLoginMethod)
        //{
        //    _login = setLoginMethod;
        //}

        //public void SetPassword(IFtpSetPassword setPasswordMethod)
        //{
        //    _password = setPasswordMethod;
        //}

        //public void SetAddress(IFtpSetAddress setAddressMethod)
        //{
        //    _address = setAddressMethod;
        //}
    }
}