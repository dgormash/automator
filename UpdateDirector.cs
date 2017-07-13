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
            _serverBuilder.BuildAddress();
            _serverBuilder.BuildPassword();
            _serverBuilder.BuildLogin();
            _serverBuilder.BuildDownloadMethod();
            _serverBuilder.BuildCheckUpdatesMethod();
            return _serverBuilder.GetFtpServer();
        }
    }
}