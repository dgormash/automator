namespace AutomatorPrg.Interfaces
{
    public interface IChecker
    {
        string CheckerLocation { get; set; }
        void StartChecking(string file);
    }
}