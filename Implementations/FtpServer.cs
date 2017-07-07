using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpServer:IFtpServer
    {
        //todo Возможно понадобятся методы доступа к полям (операциям)
        //public string Address { set { _address += value; } }
        private string _login;
        private string _password;
        private string _address;
        private string _rDirectory;

        public string CurrentDirectory
        {
            set { _rDirectory = value; }
        }

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
            _uploader.UploadFile(file, $@"{_address}\{_rDirectory}", _login, _password);
        }

        public void SetDownloadMethod(IFtpDownload downloadMethod)
        {
            _downloader = downloadMethod;
        }

        public void DownloadFile()
        {
            //todo сделать загрузку файла с ftp
        }

        public void SetCheckingMethod(IFtpCheckUploadingStatus checkingMethod)
        {
            _checkResult = checkingMethod; 
        }

        public void CheckUploadingStatus(string file)
        {
            _checkResult.CheckUploadingStatus(file, $@"{_address}\{_rDirectory}", _login, _password);
        }


        public void SetGetDirectoriesMethod(IFtpGetDirectories getDirectoriesMethod)
        {
            _directories = getDirectoriesMethod;
        }

        public List<string> GetDirectoriesList()
        {
            return _directories.GetDirectoryList(_address, _login, _password);
        }

        public void SetMakeDirectoryMethod(IFtpMakeDirectory makeDirectoryMethod)
        {
            _directoryMaker = makeDirectoryMethod;
        }

        public void MakeDirectoryOnFtpServer(string directoryToMake)
        {
            _directoryMaker.CreateDirectoryOnServer($@"{_address}\{directoryToMake}", _login,_password );
        }

        public void SetGetCurrentDirectoryMethod(IFtpGetCurrentDirectory getCurrentDirectoryMethod)
        {
            _currnetDirectory = getCurrentDirectoryMethod;
        }

        public string GetCurrentDirectory(List<string> directories)
        {
            return _currnetDirectory.GetCurrentDirectory(directories);
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
    }
}