using System.Reflection.Emit;

namespace AutomatorPrg.Interfaces
{
    public interface IFtpServer
    {
        string Login { get; set; } 
        string Password { get; set; }
        string Address { get; set; }
    }
}