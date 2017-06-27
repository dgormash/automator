namespace AutomatorPrg.Interfaces
{
    public interface IFtpUploader
    {
        UploadInfo UploadFiles(string[] files);
    }
}