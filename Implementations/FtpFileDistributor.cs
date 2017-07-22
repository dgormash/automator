using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpFileDistributor:IFtpFileDistributor
    {
        UploadedFileInfo _uplFileInfo;
        string ErrorMessage { get; set; }

        public FtpFileDistributor()
        {
            _uplFileInfo = new UploadedFileInfo();
        }
        public FileDistributionInfo DistributeFiles(string[] files)
        {
            var result = new FileDistributionInfo {Count = files.Count()};

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (fileName == null) continue;
                //выгружаем файлы в ИЦ; выгружаем оба типа файлов в один каталог на ftp-сервере
                //if (fileName.StartsWith(@"a") || fileName.StartsWith(@"f"))
                //{
                //    var icFtpServerBuilder = new IcFtpServerBuilder();
                //    icFtpServerBuilder.BuildAddress();
                //    icFtpServerBuilder.BuildLogin();
                //    icFtpServerBuilder.BuildPassword();
                //    icFtpServerBuilder.BuildUploadMethod();
                //    icFtpServerBuilder.BuildCheckingMethod();
                //    var ic = icFtpServerBuilder.GetFtpServer();
                //    ic.UploadFile(file);
                //    ic.CheckUploadingStatus(fileName);//todo предусмотреть возвращаемое значение метода
                //}

                if (fileName.StartsWith(@"v"))
                {
                    var vMoscowFtpServerBuilder = new VMoscowFtpServerBuilder();
                    //todo Вынести строительство в отдельный класс, а здесь только получать готовый ftp-сервер
                    vMoscowFtpServerBuilder.BuildAddress();
                    vMoscowFtpServerBuilder.BuildLogin();
                    vMoscowFtpServerBuilder.BuildPassword();
                    vMoscowFtpServerBuilder.BuildUploadMethod();
                    vMoscowFtpServerBuilder.BuildCheckingMethod();
                    vMoscowFtpServerBuilder.BuildGetDirectoriesMethod();
                    vMoscowFtpServerBuilder.BuildGetCurrentDirectoryMethod();
                    vMoscowFtpServerBuilder.BuildMakeDirectoryMethod();
                    var vMoscowFtp = vMoscowFtpServerBuilder.GetFtpServer();
                    var directoriesList = vMoscowFtp.GetDirectoriesList();
                    var currentDirectory = vMoscowFtp.GetCurrentDirectory(directoriesList);
                    var regex = new Regex(@"v75_001\.rar");
                    if (regex.IsMatch(fileName))
                    {
                        currentDirectory = IncreaseIndex(currentDirectory);
                        vMoscowFtp.MakeDirectoryOnFtpServer(currentDirectory);
                        vMoscowFtp.CurrentDirectory = currentDirectory;
                    }
                    vMoscowFtp.CurrentDirectory = currentDirectory;
                    vMoscowFtp.UploadFile(file);
                    vMoscowFtp.CheckUploadingStatus(fileName);//todo Предусмотреть возвращаемое значение, метод не реализован
                }
                else
                if (fileName.StartsWith(@"f"))
                {
                    var fMoscowFtpServerBuilder = new FMoscowFtpServerBuilder();
                    fMoscowFtpServerBuilder.BuildAddress();
                    fMoscowFtpServerBuilder.BuildLogin();
                    fMoscowFtpServerBuilder.BuildPassword();
                    fMoscowFtpServerBuilder.BuildUploadMethod();
                    fMoscowFtpServerBuilder.BuildCheckingMethod();
                    fMoscowFtpServerBuilder.BuildGetDirectoriesMethod();
                    fMoscowFtpServerBuilder.BuildGetCurrentDirectoryMethod();
                    fMoscowFtpServerBuilder.BuildMakeDirectoryMethod();
                    var fMoscowFtp = fMoscowFtpServerBuilder.GetFtpServer();
                    var directoriesList = fMoscowFtp.GetDirectoriesList();
                    var currentDirectory = fMoscowFtp.GetCurrentDirectory(directoriesList);
                    var regex = new Regex(@"f75_001\.rar");
                    if (regex.IsMatch(fileName))
                    {
                        currentDirectory = IncreaseIndex(currentDirectory);
                        fMoscowFtp.MakeDirectoryOnFtpServer(currentDirectory);
                        fMoscowFtp.CurrentDirectory = currentDirectory;
                    }
                    fMoscowFtp.CurrentDirectory = currentDirectory;
                    fMoscowFtp.UploadFile(file);
                    fMoscowFtp.CheckUploadingStatus(fileName);//todo Предусмотреть возвращаемое значение, метод не реализован
                }
                else if (fileName.StartsWith(@"a"))
                {
                    var aMoscowFtpServerBuilder = new AMoscowFtpServerBuilder();
                    aMoscowFtpServerBuilder.BuildAddress();
                    aMoscowFtpServerBuilder.BuildLogin();
                    aMoscowFtpServerBuilder.BuildPassword();
                    aMoscowFtpServerBuilder.BuildUploadMethod();
                    aMoscowFtpServerBuilder.BuildCheckingMethod();
                    aMoscowFtpServerBuilder.BuildGetDirectoriesMethod();
                    aMoscowFtpServerBuilder.BuildGetCurrentDirectoryMethod();
                    aMoscowFtpServerBuilder.BuildMakeDirectoryMethod();
                    var aMoscowFtp = aMoscowFtpServerBuilder.GetFtpServer();
                    var directoriesList = aMoscowFtp.GetDirectoriesList();
                    var currentDirectory = aMoscowFtp.GetCurrentDirectory(directoriesList);
                    var regex = new Regex(@"a75_001\.rar");
                    if (regex.IsMatch(fileName))
                    {
                        currentDirectory = IncreaseIndex(currentDirectory);
                        aMoscowFtp.MakeDirectoryOnFtpServer(currentDirectory);
                        aMoscowFtp.CurrentDirectory = currentDirectory;
                    }
                    aMoscowFtp.CurrentDirectory = currentDirectory;
                    aMoscowFtp.UploadFile(file);
                    aMoscowFtp.CheckUploadingStatus(fileName);//todo Предусмотреть возвращаемое значение, метод не реализован
                }
                //else if (fileName.StartsWith(@"a"))
                //{
                //    var testFtpServerBuilder = new TestFtpServerBuilder();
                //    testFtpServerBuilder.BuildAddress();
                //    testFtpServerBuilder.BuildLogin();
                //    testFtpServerBuilder.BuildPassword();
                //    testFtpServerBuilder.BuildUploadMethod();
                //    testFtpServerBuilder.BuildCheckingMethod();
                //    testFtpServerBuilder.BuildGetDirectoriesMethod();
                //    testFtpServerBuilder.BuildGetCurrentDirectoryMethod();
                //    testFtpServerBuilder.BuildMakeDirectoryMethod();
                //    var testFtp = testFtpServerBuilder.GetFtpServer();
                //    var directoriesList = testFtp.GetDirectoriesList();
                //    var currentDirectory = testFtp.GetCurrentDirectory(directoriesList);
                //    var regex = new Regex(@"a75_001\.rar");
                //    if (regex.IsMatch(fileName))
                //    {
                //        currentDirectory = IncreaseIndex(currentDirectory);
                //        testFtp.MakeDirectoryOnFtpServer(currentDirectory);
                //        testFtp.CurrentDirectory = currentDirectory;
                //    }
                //    testFtp.CurrentDirectory = currentDirectory;
                //    testFtp.UploadFile(file);
                //    testFtp.CheckUploadingStatus(fileName);//todo Предусмотреть возвращаемое значение, метод не реализован
                //}
            }
            return result;
        }

        private static string IncreaseIndex(string currentDirectory)
        {
            var match = Regex.Match(currentDirectory, "[0-9]");
            var index = Convert.ToByte(match.Value);
            return $"R{Convert.ToString(++index)}";
        }
    }
}