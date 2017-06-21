using System.Collections.Generic;
using System.Linq;
using AutomatorPrg.Implementations;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg
{
    public class Client
    {
        private IFtpUpdateCheker ftpUpdateCheker; //если взвращает true, есть обновление, запускаем updater
        private IUpdater updater;
        private IListReturner listReturner;
        //private IChecker _checkerCreator;
        private IResultChecker resultChecker;
        private IErrorRemover errorRemover;
        private IProtocolWriter protocolWriter;
        private IFtpUploader ftpUploader;
        private IFtpDownloader ftpDownloader;
        private IGarbageCollector garbageCollector;

        private ICheckerCreator _checkerCreator;
        private IErrorFinderCreator _errorFinderCreator;
        private IArchiveFinderCreator _archiveFinderCreator;
        private IErrorRemoverCreator _errorRemoverCreator;

        public string FilePath {private get; set; }
        public string Mask { private get; set; }
        public string CurrentChecker { private get; set; }

        public byte Execute()
        {
            var result = 0;

            #region Инстанцируем класс, возвращающий список файлов

            listReturner = new ListReturner();
            var fileList = listReturner.GetFileList(FilePath, Mask);
            #endregion

            #region Инстанцируем чеккер

            _checkerCreator = new CommonCheckerCreator();
            var checker = _checkerCreator.CreateChecker();
            checker.CheckerLocation = CurrentChecker;
            foreach (var file in fileList)
            {
                checker.StartChecking(file);
            }
            #endregion

            #region Инстанцируем класс, проверяющий результаты

            _errorFinderCreator = new ErrorFinderCreator();
            var errorList = _errorFinderCreator.Create();
            var errors = errorList.FindErrors(FilePath);
            var err = errors as string[] ?? errors.ToArray();
            if (err.Length != 0)
            {
                //todo инстанцирование класса, реалезующего удаление ошибок
                foreach (var e in err)
                {
                    //todo удаление ошибок после проверки чеккером
                }
            }

            _archiveFinderCreator = new ArchiveFinderCreator();
            var archList = _archiveFinderCreator.Create();
            var archives = archList.FindArchives(FilePath);
            var arch = archives as string[] ?? archives.ToArray();
            if (arch.Length!= 0)
            {
                //todo инстанцирование класса, осуществляющего выгрузку архивов на ftp
                foreach (var a in arch)
                {
                    //todo выгрузка файлов на ftp
                }
            }
            #endregion

            return (byte) result;
        }
    }
}