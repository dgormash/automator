using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpUploaderCreator:IFtpUploaderCreator
    {
        public IFtpUploader Create()
        {
            return new FtpFileDistributor();
        }
    }
}