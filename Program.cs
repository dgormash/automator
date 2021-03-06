﻿using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using AutomatorPrg.Implementations;

namespace AutomatorPrg
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"Automator, ver. 2.0.2.0 от 31.07.2017 г. РЕЖИМ ОПЫТНОЙ ЭКСПЛУАТАЦИИ!!!");
            Console.ResetColor();

            var client = new Client(new CommonCheckerCreator(), //todo выбор чеккера по ключам
                new ErrorFinderCreator(),
                new ArchiveFinderCreator(),
                new ErrorRemoverCreator(),
                new GarbageCollectorCreator(),
                new FileAppenderCreator(),
                new FtpFileDistributorCreator())
            {
                CheckerLocation = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\misc\chkNewArv.exe", //путь до чеккера
                FilePath = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\in", //путь до каталога in
            };

            switch (args[0])
            {
                default:
                    Console.WriteLine(@"Указан неверный ключ. Приложение будет остановлено...");
                    Environment.Exit(0);
                    break;
                case @"-v":
                    client.Mask = @"V75A*.???";
                    client.Additive = $@"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\add\V75A6FD.000";
                    break;
                case @"-f":
                    client.Mask = @"F75A*.???";
                    client.Additive = $@"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\add\F75A6FD.000";
                    break;
                case @"-a":
                    client.Mask = @"A75A*.???";
                    client.Additive = $@"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\add\A75A0FD.000";
                    break;
                case @"-t":
                    client.Mask = @"A75A*.???";
                    client.Additive = $@"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\add\A75A0FD.000";
                    break;
            }
               


            client.Execute(); //todo анализ кода завершения
           
        }
    }
  }
