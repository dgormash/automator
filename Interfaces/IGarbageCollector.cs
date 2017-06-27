namespace AutomatorPrg.Interfaces
{
    public interface IGarbageCollector
    {
        string MoveTo { get; set; }
        void CleanUp(string path, string mask);
    }
}