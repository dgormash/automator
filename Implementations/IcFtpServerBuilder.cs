using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class IcFtpServerBuilder:IFtpServerBuilder
    {
        private readonly FtpServer _ftp;
        public IcFtpServerBuilder()
        {
            _ftp = new FtpServer();
        }

        public void BuildUploadMethod()
        {
            _ftp.SetUploadMethod(new FtpFileUploader());
        }

        public void BuildDownloadMethod()
        {
            _ftp.SetDownloadMethod(new FtpFileDownloader());
        }

        public void BuildCheckingMethod()
        {
            _ftp.SetCheckingMethod(new FtpFileStatusChecker());
        }

        public void BuildGetDirectoriesMethod()
        {
           _ftp.SetGetDirectoriesMethod(new FtpDirectoryListRetriever());
        }

        public void BuildMakeDirectoryMethod()
        {
            _ftp.SetMakeDirectoryMethod(new FtpDirectoryCreator());
        }

        public void BuildGetCurrentDirectoryMethod()
        {
            _ftp.SetGetCurrentDirectoryMethod(new FtpCurrentDirectoryRetriever());
        }

        public void BuildLogin()
        {
           _ftp.SetLogin(@"gaiobmen");
        }

        public void BuildPassword()
        {
            _ftp.SetPassword(@"cntggkth");
        }

        public void BuildAddress()
        {
            _ftp.SetAddress(@"ftp://vc_ic.uvd.chel.su/%2fusers/obmen.zad/obmen.gai");
        }

        public FtpServer GetFtpServer()
        {
            return _ftp;
        }
    }

   
}