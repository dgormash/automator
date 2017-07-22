using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using AutomatorPrg.Implementations;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg
{
    public class Client
    {
        private Subject _subject;
        private ConsoleReporter _consoleReporter;
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

        private FtpServer _updateServer;

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
            _subject = new Subject();
            _consoleReporter = new ConsoleReporter(_subject);
        }
        public byte Execute()
        {
            var result = 0;


            var checker = _checkerCreator.CreateChecker();
            checker.CheckerLocation = CheckerLocation;

            var errorList = _errorFinderCreator.Create();

            var remover = _errorRemoverCreator.Create();

            var collector = _garbageCollectorCreator.Create();

            #region Обновление софта
            var updateDirector = new UpdateDirector(new UpdateFtpServerBuilder());
            _updateServer = updateDirector.BuildServer();

            var miscFiles = new[]
            {
                $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\misc\chknewarv.exe",
                $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\misc\podr.gz"
            };

            _subject.SetUpMessage(@"Поиск обновлений на ftp://10.7.97.20/software/registr");

            foreach (var file in miscFiles)
            {
                var checkReslut = _updateServer.CheckFileState(file);
                if (checkReslut <= File.GetLastWriteTime(file)) continue;
                _subject.SetUpMessage($@"Найдено обновление файла {Path.GetFileName(file)} от {checkReslut}");
                _updateServer.UpdateFile(file);
            }
            #endregion

            //2. Дополнили файл, если есть, чем дополнять
            var appender = _fileAppenderCreator.Create();
            

            //3. Обрабатываем файлы
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
                    var retMessage = appender.AppendFile(file, Additive);
                    var fileName = Path.GetFileName(file);
                    if (retMessage != null)
                    {
                       _subject.SetUpMessage($@"К файлу {fileName} {retMessage}");
                    }
                    _subject.SetUpMessage($@"Запускаю проверку файла {fileName} программой chkNewArv.exe...");
                    checker.StartChecking(file);
                }

                //Получаем список файлов с ошибками и для каждого из файлов в списке производим удаление ошибок
                var errors = errorList.FindErrors(FilePath);
                var err = errors as string[] ?? errors.ToArray();
                if (err.Length != 0)
                {
                    _subject.SetUpMessage(@"Обнаружены следующие отчёты об ошибках:");
                    var i = 0;
                    foreach (var file in err)
                    {
                        _subject.SetUpMessage($"{++i}) {Path.GetFileName(file)}");
                    }
                    remover.RemoveErrors(err, FilePath);
                }
                else
                {
                    _doIteration = false;
                }

                collector.MoveTo = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\dispose"; 
                collector.CleanUp(FilePath, "*.txt");
                collector.CleanUp(FilePath, "*.old");
                collector.MoveTo = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\err";
                collector.CleanUp(FilePath, "*.err");
            }


            var archList = _archiveFinderCreator.Create();
            var archives = archList.FindArchives(FilePath);
            var arch = archives as string[] ?? archives.ToArray();
            if (arch.Length != 0)
            {
                _subject.SetUpMessage(@"Обнаружены следующие архивы:");
                var i = 0;
                foreach (var file in arch)
                {
                    _subject.SetUpMessage($"{++i}) {Path.GetFileName(file)}");
                }
                _ftpFileDistributor = _ftpFileDistributorCreator.Create();
                var uploadResult = _ftpFileDistributor.DistributeFiles(arch);
                MoveToArchive(arch);
            //todo uploadResult вывести в протокол выполнения и на экран
            }
            
          
            return (byte) result;
        }

        private static void MoveToArchive(IEnumerable<string> archs)
        {
            foreach (var arch in archs)
            {
                var archInfo = new FileInfo(arch);
                if (archInfo.Name.StartsWith("a", true, CultureInfo.CurrentCulture))
                {
                    archInfo.MoveTo($@"G:\ADM.DBF\out\COMMON\ora\arh\{archInfo.Name}");
                }

                if (archInfo.Name.StartsWith("f", true, CultureInfo.CurrentCulture))
                {
                    archInfo.MoveTo($@"G:\amt.dbf\out\GIC1\arh\{archInfo.Name}");
                }

                if (archInfo.Name.StartsWith("v", true, CultureInfo.CurrentCulture))
                {
                    archInfo.MoveTo($@"G:\TASKS.EXE\VUD.EXE\FIS_VUD\arh\{DateTime.Now.Year}\{archInfo.Name}");
                } 
            }
        }

       
    }
}