using AutomatorPrg.Implementations;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg
{
    internal class UpdateDirector
    {
        private readonly IFtpServerBuilder _serverBuilder;
        public UpdateDirector(IFtpServerBuilder builder)
        {
            _serverBuilder = builder;
        }

        public FtpServer BuildServer()
        {
            return _serverBuilder.GetFtpServer();
        }
    }
}