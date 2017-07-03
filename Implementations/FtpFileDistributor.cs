using System;
using System.IO;
using System.Net;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpFileDistributor:IFtpFileDistributor
    {
        
        public FileDstributionInfo DistributeFiles(string[] files)
        {
            var result = new FileDstributionInfo();
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (fileName == null) continue;
                //выгружаем файлы в ИЦ; выгружаем оба файла в один каталог на ftp-сервере
                if (fileName.StartsWith(@"a") || fileName.StartsWith(@"f"))
                {
                    var icFtpServerBuilder = new IcFtpServerBuilder();
                    icFtpServerBuilder.BuildAddress();
                    icFtpServerBuilder.BuildLogin();
                    icFtpServerBuilder.BuildPassword();
                    icFtpServerBuilder.BuildUploadMethod();
                    icFtpServerBuilder.BuildCheckingMethod();
                    var ic = icFtpServerBuilder.GetFtpServer();
                    ic.UploadFile(file);
                    ic.CheckUploadingStatus(file);//todo предусмотреть возвращаемое значение метода
                }

                if (fileName.StartsWith(@"v"))
                {
                    var vMoscowFtpServerBuilder = new VMoscowFtpServerBuilder();
                    vMoscowFtpServerBuilder.BuildAddress();
                    vMoscowFtpServerBuilder.BuildLogin();
                    vMoscowFtpServerBuilder.BuildPassword();
                    vMoscowFtpServerBuilder.BuildUploadMethod();
                    vMoscowFtpServerBuilder.BuildCheckingMethod();
                    vMoscowFtpServerBuilder.BuildGetDirectoriesMethod();
                    vMoscowFtpServerBuilder.BuildGetCurrentDirectoryMethod();
                    var vMoscowFtp = vMoscowFtpServerBuilder.GetFtpServer();
                    //todo Получаем список директорий (возвращает список каталогов)
                    //todo Из полученного списка вычисляем текущий каталог (возвращает строку с именем текущего каталога)
                    //todo Получаем порядковый номер файла, если порядковый номер файла равен 001, то создаём на сервере новый каталог (текущий каталог +1)
                    vMoscowFtp.UploadFile(file);
                    vMoscowFtp.CheckUploadingStatus(file);//todo Предусмотреть возвращаемое значение
                }
                else if (fileName.StartsWith(@"f"))
                {
                    var fMoscowFtpServerBuilder = new FMoscowFtpServerBuilder();
                    fMoscowFtpServerBuilder.BuildAddress();
                    fMoscowFtpServerBuilder.BuildLogin();
                    fMoscowFtpServerBuilder.BuildPassword();
                    fMoscowFtpServerBuilder.BuildUploadMethod();
                    fMoscowFtpServerBuilder.BuildCheckingMethod();
                    fMoscowFtpServerBuilder.BuildGetDirectoriesMethod();
                    fMoscowFtpServerBuilder.BuildGetCurrentDirectoryMethod();
                    var fMoscowFtp = fMoscowFtpServerBuilder.GetFtpServer();
                    //todo Получаем список директорий (возвращает список каталогов)
                    //todo Из полученного списка вычисляем текущий каталог (возвращает строку с именем текущего каталога)
                    //todo Получаем порядковый номер файла, если порядковый номер файла равен 001, то создаём на сервере новый каталог (текущий каталог +1)
                    fMoscowFtp.UploadFile(file);
                    fMoscowFtp.CheckUploadingStatus(file); //todo Предусмотреть возвращаемое значение
                }
                else if(fileName.StartsWith(@"a"))
                {
                    var aMoscowFtpServerBuilder = new AMoscowFtpServerBuilder();
                    aMoscowFtpServerBuilder.BuildAddress();
                    aMoscowFtpServerBuilder.BuildLogin();
                    aMoscowFtpServerBuilder.BuildPassword();
                    aMoscowFtpServerBuilder.BuildUploadMethod();
                    aMoscowFtpServerBuilder.BuildCheckingMethod();
                    aMoscowFtpServerBuilder.BuildGetDirectoriesMethod();
                    aMoscowFtpServerBuilder.BuildGetCurrentDirectoryMethod();
                    var aMoscowFtp = aMoscowFtpServerBuilder.GetFtpServer();
                    //todo Получаем список директорий (возвращает список каталогов)
                    //todo Из полученного списка вычисляем текущий каталог (возвращает строку с именем текущего каталога)
                    //todo Получаем порядковый номер файла, если порядковый номер файла равен 001, то создаём на сервере новый каталог (текущий каталог +1)
                    aMoscowFtp.UploadFile(file);
                    aMoscowFtp.CheckUploadingStatus(file); //todo Предусмотреть возвращаемое значение
                }
            }
            return result;
        }

        
    }
}