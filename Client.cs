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
        private IGarbageCollectorCreator _garbageCollectorCreator;
        private IFileAppenderCreator _fileAppenderCreator;
        private IFtpUploaderCreator _ftpUploaderCreator;
        private bool _doIteration;

        public string FilePath {private get; set; }
        public string Additive { private get; set; }
        public string Mask { private get; set; }
        public string CurrentChecker { private get; set; }

        public byte Execute()
        {
            var result = 0;

            #region Инстанцируем класс, возвращающий список файлов

            listReturner = new ListReturner();
            IEnumerable<string> fileList = null;
            //var fileList = listReturner.GetFileList(FilePath, Mask);
            #endregion

            _checkerCreator = new CommonCheckerCreator();
            var checker = _checkerCreator.CreateChecker();
            checker.CheckerLocation = CurrentChecker;
            

            _errorFinderCreator = new ErrorFinderCreator();
            var errorList = _errorFinderCreator.Create();

            _errorRemoverCreator = new ErrorRemoverCreator();
            var remover = _errorRemoverCreator.Create();
            IEnumerable<string> errors = null;

            _garbageCollectorCreator = new GarbageCollectorCreator();
            var collector = _garbageCollectorCreator.Create();

            //1. Дополнили файл, если есть, чем дополнять
            _fileAppenderCreator = new FileAppenderCreator();
            var appender = _fileAppenderCreator.Create();
            appender.AppendFile(FilePath, Additive);

            _doIteration = true;
            while (_doIteration)
            {
                //todo Провести оптимизацию создания переменных
                //Получаем список файлов
                fileList = listReturner.GetFileList(FilePath, Mask);

                //Для каждого файла из списка запускем чеккер. В результате появятся либо архивы, либо останутся исходные файлы и
                //появятся новые файлы с расширением .txt
                foreach (var file in fileList)
                {
                    checker.StartChecking(file);
                }

                //Получаем список ошибок и для каждого из файлов в списке производим удаление ошибок
                errors = errorList.FindErrors(FilePath);
                var err = errors as string[] ?? errors.ToArray();
                if (err.Length != 0)
                {
                    remover.RemoveErrors(err);
                }
                else
                {
                    _doIteration = false;
                }

                //todo Организовать заполнения свойства MoveTo
                collector.MoveTo = ""; //dispose directory
                collector.CleanUp(FilePath, "*.txt");
                collector.CleanUp(FilePath, "*.old");
                collector.MoveTo = ""; //err directory
                collector.CleanUp(FilePath, "*.err");
            }



            _archiveFinderCreator = new ArchiveFinderCreator();
                var archList = _archiveFinderCreator.Create();
                var archives = archList.FindArchives(FilePath);
                var arch = archives as string[] ?? archives.ToArray();
                if (arch.Length != 0)
                {
                   _ftpUploaderCreator=new FtpUploaderCreator() ;
                    ftpUploader = _ftpUploaderCreator.Create();
                    var uploadResult = ftpUploader.UploadFiles(arch);
                //todo uploadResult вывести в протокол выполнения и на экран
                }
            
          
            return (byte) result;
        }
    }
}