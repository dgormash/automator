using System;
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

        private readonly Subject _subject;
        private readonly ProgramStoperSubject _stoperSubject;

        public FtpServer()
        {
            _subject = new Subject();
            _stoperSubject = new ProgramStoperSubject();
            var consoleReporter = new ConsoleReporter(_subject);
            var programStoper = new ProgramStoperObserver(_stoperSubject);
        }

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
            if (_uploader.UploadFile(file, $@"{_address}\{_rDirectory}", _login, _password) == FtpCommandStatus.NotOk)
            {
                _subject.SetUpMessage(_uploader.ErrorMessage);
                _stoperSubject.StopProgramByEmergency();
            }

        }

        public void SetDownloadMethod(IFtpDownload downloadMethod)
        {
            _downloader = downloadMethod;
        }

        public void UpdateFile(string file)
        {
            if (_downloader.DownloadFile($@"{_address}\{Path.GetFileName(file)}", file, _login, _password) == FtpCommandStatus.NotOk)
            {
                _subject.SetUpMessage( _downloader.ErrorMessage);
                _stoperSubject.StopProgramByEmergency();
            }
        }

        public void SetCheckingMethod(IFtpCheckUploadingStatus checkingMethod)
        {
            _checkResult = checkingMethod; 
        }

        public void CheckUploadingStatus(string file)
        {
            _subject.SetUpMessage(
                _checkResult.CheckUploadingStatus(file, $@"{_address}\{_rDirectory}", _login, _password) ==
                FtpCommandStatus.NotOk
                    ? _checkResult.ErrorMessage
                    : $@"Файл {_checkResult.FileName} успешно выгружен; размер: {_checkResult.FileSize};");
        }


        public void SetGetDirectoriesMethod(IFtpGetDirectories getDirectoriesMethod)
        {
            _directories = getDirectoriesMethod;
        }

        public List<string> GetDirectoriesList()
        {
            var result = _directories.GetDirectoryList(_address, _login, _password);
            if (result != null) return result;
            _subject.SetUpMessage($"Я не смог получить список каталогов на {_address}");
            _subject.SetUpMessage($"Метод GetDirectoryList: {_directories.ErrorMessage}");
            _stoperSubject.StopProgramByEmergency();
            return null;
        }

        public void SetMakeDirectoryMethod(IFtpMakeDirectory makeDirectoryMethod)
        {
            _directoryMaker = makeDirectoryMethod;
        }

        public void MakeDirectoryOnFtpServer(string directoryToMake)
        {
            if (_directoryMaker.CreateDirectoryOnServer($@"{_address}\{directoryToMake}", _login, _password) ==
                FtpCommandStatus.NotOk)
            {
                _subject.SetUpMessage(_directoryMaker.ErrorMessage);
                _stoperSubject.StopProgramByEmergency();
            }
        }

        public void SetGetCurrentDirectoryMethod(IFtpGetCurrentDirectory getCurrentDirectoryMethod)
        {
            _currnetDirectory = getCurrentDirectoryMethod;
        }

        public void SetCheckUpdatesMethod(IFtpGetFileInfo getFileInfoMethod)
        {
            _fileInfoGetter = getFileInfoMethod;
        }

        public DateTime CheckFileState(string file)
        {
            if (_fileInfoGetter.GetFileInfo($@"{_address}\{Path.GetFileName(file)}", _login, _password) ==
                FtpCommandStatus.NotOk)
            {
                _subject.SetUpMessage(_fileInfoGetter.ErrorMessage);
            }
            return _fileInfoGetter.LastModified;
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