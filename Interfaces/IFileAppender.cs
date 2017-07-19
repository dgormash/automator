namespace AutomatorPrg.Interfaces
{
    public interface IFileAppender
    {
        string AppendFile(string outgoingFile, string additiveFile);
    }
}