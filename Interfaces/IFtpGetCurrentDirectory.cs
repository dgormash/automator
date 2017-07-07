using System.Collections.Generic;

namespace AutomatorPrg.Interfaces
{
    public interface IFtpGetCurrentDirectory
    {
        string GetCurrentDirectory(IEnumerable<string> list);
    }
}