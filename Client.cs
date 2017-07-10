using System.Linq;
using AutomatorPrg.Implementations;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg
{
    public class Client
    {
        private IListReturner _listReturner;
        private IFtpFileDistributor _ftpFileDistributor;
        private readonly ICheckerCreator _checkerCreator;
        private readonly IErrorFinderCreator _errorFinderCreator;
        private readonly IArchiveFinderCreator _archiveFinderCreator;
        private readonly IErrorRemoverCreator _errorRemoverCreator;
        private readonly IGarbageCollectorCreator _garbageCollectorCreator;
        private readonly IFileAppenderCreator _fileAppenderCreator;
        private readonly IFtpFileDistributorCreator _ftpFileDistributorCreator;
        private bool _doIteration;

        public string FilePath {private get; set; }
        //Путь к файлу, в котором содержатся добавочные строки
        public string Additive { private get; set; }
        public string Mask { private get; set; }
        //Путь к файлу chkNewArv.exe (чеккер)
        public string CheckerLocation { private get; set; }

        public Client(ICheckerCreator checkerCreator, 
            IErrorFinderCreator errorFinderCreator, 
            IArchiveFinderCreator archiveFinderCreator, 
            IErrorRemoverCreator errorRemoverCreator,
            IGarbageCollectorCreator garbageCollectorCreator, 
            IFileAppenderCreator fileAppenderCreator, 
            IFtpFileDistributorCreator ftpFileDistributorCreator)
        {
            _checkerCreator = checkerCreator;
            _errorFinderCreator = errorFinderCreator;
            _archiveFinderCreator = archiveFinderCreator;
            _errorRemoverCreator = errorRemoverCreator;
            _garbageCollectorCreator = garbageCollectorCreator;
            _fileAppenderCreator = fileAppenderCreator;
            _ftpFileDistributorCreator = ftpFileDistributorCreator;
        }
        public byte Execute()
        {
            var result = 0;


            var checker = _checkerCreator.CreateChecker();
            checker.CheckerLocation = CheckerLocation;

            var errorList = _errorFinderCreator.Create();

            var remover = _errorRemoverCreator.Create();

            var collector = _garbageCollectorCreator.Create();

            //1. Дополнили файл, если есть, чем дополнять
            var appender = _fileAppenderCreator.Create();
            appender.AppendFile(FilePath, Additive);

            _listReturner = new ListReturner();
            _doIteration = true;
            while (_doIteration)
            {
                //Получаем список файлов
                var fileList = _listReturner.GetFileList(FilePath, Mask);

                //Для каждого файла из списка запускем чеккер. В результате появятся либо архивы, либо останутся исходные файлы и
                //появятся новые файлы с расширением .txt
                foreach (var file in fileList)
                {
                    checker.StartChecking(file);
                }

                //Получаем список ошибок и для каждого из файлов в списке производим удаление ошибок
                var errors = errorList.FindErrors(FilePath);
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


            var archList = _archiveFinderCreator.Create();
            var archives = archList.FindArchives(FilePath);
            var arch = archives as string[] ?? archives.ToArray();
            if (arch.Length != 0)
            {
                _ftpFileDistributor = _ftpFileDistributorCreator.Create();
                var uploadResult = _ftpFileDistributor.DistributeFiles(arch);
            //todo uploadResult вывести в протокол выполнения и на экран
            }
            
          
            return (byte) result;
        }
    }
}