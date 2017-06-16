using System;

namespace AutomatorPrg
{
    class ConsoleMessage
    {
        public void Write(string msg, string type)
        {
            SetConsoleMsgProperty(type);
            Console.Write(msg);
            SetConsoleMsgProperty(@"Normal");
        }

        public void WriteLine(string msg, string type)
        {
            SetConsoleMsgProperty(type);
            Console.WriteLine(msg);
            SetConsoleMsgProperty(@"Normal");
        }

        private void SetConsoleMsgProperty(string type)
        {
            switch (type)
            {
                case @"Confirm":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case @"Alert":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case @"Error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case @"Message":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case @"Normal":
                    Console.ResetColor();
                    break;
            }
        }
    }
}
