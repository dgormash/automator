using System;
using System.IO;
using System.Net;
using AutomatorPrg.Interfaces;

namespace AutomatorPrg.Implementations
{
    public class FtpFileDistributor:IFtpFileDistributor
    {
        
        public FileDstributionInfo DistributeFiles(string[] files)
        {
            var result = new FileDstributionInfo();
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (fileName == null) continue;
                if (fileName.StartsWith(@"a") || fileName.StartsWith(@"f"))
                {
                    var icFtpServerBuilder = new IcFtpServerBuilder();
                    icFtpServerBuilder.BuildAddress();
                    icFtpServerBuilder.BuildLogin();
                    icFtpServerBuilder.BuildPassword();
                    icFtpServerBuilder.BuildUploadMethod();
                    icFtpServerBuilder.BuildCheckingMethod();
                    var ic = icFtpServerBuilder.GetFtpServer();
                    ic.UploadFile(file);
                }

                if (fileName.StartsWith(@"v"))
                {
                    
                }
            }
            return result;
        }

        
    }
}