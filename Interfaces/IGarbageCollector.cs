namespace AutomatorPrg.Interfaces
{
    public interface IGarbageCollector
    {
        string TrashDirectory { get; set; }
        string ErrorDirectory { get; set; }
        void CleanUp(string path);
    }
}