using System;
using System.Reflection;
using AutomatorPrg.Implementations;

namespace AutomatorPrg
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"Automator, ver. 2.0.0.0, здесь будет дата билда");
            Console.ResetColor();

            //todo организовать работу с ключами, вывод общей информации в консоль и в файл протокола
            var client = new Client(new CommonCheckerCreator(), //todo выбор чеккера по ключам
                new ErrorFinderCreator(),
                new ArchiveFinderCreator(),
                new ErrorRemoverCreator(),
                new GarbageCollectorCreator(),
                new FileAppenderCreator(),
                new FtpFileDistributorCreator())
            {
                Additive = string.Empty, //заполняется по ключу, с которым запускалось приложение, путь к файлу .000 
                CheckerLocation = $@"{Assembly.GetExecutingAssembly().Location}\misc\chkNewArv.exe", //путь до чеккера
                FilePath = $@"{Assembly.GetExecutingAssembly().Location}\in", //путь до каталога in
                Mask = string.Empty // заполняется по ключу, с которым запускалось приложение
            };


            client.Execute(); //todo анализ кода завершения
           
        }
    }
  }
