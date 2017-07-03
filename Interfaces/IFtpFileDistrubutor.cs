namespace AutomatorPrg.Interfaces
{
    public interface IFtpFileDistributor
    {
        FileDstributionInfo DistributeFiles(string[] files);
    }
}