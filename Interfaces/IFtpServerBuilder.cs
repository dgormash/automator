namespace AutomatorPrg.Interfaces
{
    public interface IFtpServerBuilder
    {
        void BuildUploadMethod();
        void BuildDownloadMethod();
        void BuildCheckingMethod();
        void BuildGetDirectoriesMethod();
        void BuildMakeDirectoryMethod();
        void BuildGetCurrentDirectoryMethod();
        void BuildLogin();
        void BuildPassword();
        void BuildAddress();

        IFtpServer GetFtpServer();
    }
}