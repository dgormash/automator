namespace AutomatorPrg.Interfaces
{
    public interface IFtpFileDistributor
    {
        FileDistributionInfo DistributeFiles(string[] files);
    }
}