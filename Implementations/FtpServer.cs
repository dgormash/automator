using System.Collections.Generic;
using System.IO;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpServer:IFtpServer
    {
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
        private IFtpGetFileInfo _fileInfoGetter;
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

        public void UpdateFile(string file)
        {
            _downloader.DownloadFile($@"{_address}\{Path.GetFileName(file)}", file, _login, _password);
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

        public void SetCheckUpdatesMethod(IFtpGetFileInfo getFileInfoMethod)
        {
            _fileInfoGetter = getFileInfoMethod;
        }

        public FtpFileInfo CheckFileState(string file)
        {
            _fileInfoGetter.GetFileInfo($@"{_address}\{Path.GetFileName(file)}", _login, _password);
            return _fileInfoGetter.Result;
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