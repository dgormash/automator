using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class TestFtpServerBuilder:IFtpServerBuilder
    {
        private readonly FtpServer _ftp;

        public TestFtpServerBuilder()
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
            _ftp.SetLogin(@"roland");
        }

        public void BuildPassword()
        {
            _ftp.SetPassword(@"grOsSer1l0");
        }

        public void BuildAddress()
        {
            _ftp.SetAddress(@"ftp://10.196.144.200/ugai/files/roland/ftp/ADMN");
        }

        public void BuildCheckUpdatesMethod()
        {
            throw new System.NotImplementedException();
        }

        public FtpServer GetFtpServer()
        {
            return _ftp;
        }
    }
}