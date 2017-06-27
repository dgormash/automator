namespace AutomatorPrg.Interfaces
{
    public interface IFileAppender
    {
        void AppendFile(string outgoingFile, string additiveFile);
    }
}