using System.Reflection;
using AutomatorPrg.Implementations;
using AutomatorPrg.Interfaces;
using Microsoft.SqlServer.Server;

namespace AutomatorPrg
{
    public class MiscFiles
    {
        IFtpSoftUpdater _ftpSoftUpdater;
        private string _checker;
        private string _podr;

        public MiscFiles()
        {
            _checker = $@"{Assembly.GetExecutingAssembly().Location}\misc\chknewarv.exe";
            _podr = $@"{Assembly.GetExecutingAssembly().Location}\misc\podr.gz";
            _ftpSoftUpdater = new FtpSoftUpdater();
        }

        private bool CheckUpdate(string file)
        {
            return true;
        }

        private void MakeUpdate()
        {
            
        }

    }
}