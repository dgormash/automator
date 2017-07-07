using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class VMoscowFtpServerBuilder:IFtpServerBuilder
    {
       private readonly FtpServer _ftp;

        public VMoscowFtpServerBuilder()
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
            _ftp.SetLogin(@"mrc_1165");
        }

        public void BuildPassword()
        {
           _ftp.SetPassword(@"mrc_ftp_1165");
        }

        public void BuildAddress()
        {
            _ftp.SetAddress(@"ftp://10.7.97.20/MRC_UR/1175/VOD");
        }

        public FtpServer GetFtpServer()
        {
            return _ftp;
        }
    }
}