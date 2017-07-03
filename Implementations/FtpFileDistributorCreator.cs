using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpFileDistributorCreator:IFtpFileDistributorCreator

    {
        public IFtpFileDistributor Create()
        {
            return  new FtpFileDistributor();
        }
    }
}