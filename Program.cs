using System;
using System.Reflection;
using AutomatorPrg.Implementations;

namespace AutomatorPrg
{
    class Program
    {
        static void Main(string[] args)
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
            // #region Params cheking

            // var doFtp = true;
            // var doWaiting = true;
            // var withPrompt = false;



            // if (args.Length != 0)
            // {

            //     foreach (string param in args)
            //     {

            //         switch (param)
            //         {
            //             case @"-noftp":
            //                 doFtp = false;
            //                 break;
            //             case @"-nowait":
            //                 doWaiting = false;
            //                 break;
            //             case @"-upr":
            //                 withPrompt = true;
            //                 break;
            //             case @"-h":
            //                 Console.WriteLine(@"Немного помощи:");
            //                 Console.WriteLine(@"-noftp - не выгружаем архивы после запаковки.");
            //                 Console.WriteLine(@"-nowait - не нажимаем любую кнопку по окончании работы.");
            //                 Console.WriteLine(@"-upr - выгружаем архивы только после подтверждения.");
            //                 Console.WriteLine(@"-h - сия помощь.");
            //                 Console.WriteLine(@"А сейчас, извините, работа программы будет прекращена, пока вы не решите, какой из параметров вам предпочтительней");
            //                 Console.ReadKey();
            //                 Environment.Exit(0);
            //                 break;
            //             default:
            //                 Console.ForegroundColor = ConsoleColor.Yellow;
            //                 Console.WriteLine("Параметр [{0}] не распознан.", param);
            //                 Console.ResetColor();
            //                 break;
            //         }
            //     }
            // }

            // #endregion

            // /*Получаем путь текущего каталога (каталога, из которого запускается программа)*/
            // var exePath = Directory.GetCurrentDirectory();

            // /*При запуске удаляем всё, что есть в \out, чтобы нам не мешали старые архивы, которые могли остаться*/
            // var delList = new FLister(string.Format(@"{0}\out", exePath), @"?75_???.rar");
            // if (delList.FList != null)
            // {
            //     foreach (string arch in delList.FList)
            //     {
            //         File.Delete(arch);
            //     }
            // }

            // #region Creating report

            // ReportWriter report = new ReportWriter(DateTime.Now.ToString("ddMMyyyy"), exePath);
            // report.WriteToFile(string.Format(@"Старт программы {0}", DateTime.Now.ToString("dd MMMM yyyy, ddd в HH:mm")));
            // report.WriteToFile(new string('-', 70));

            // #endregion

            // ConsoleMessage cMsg = new ConsoleMessage();
            // //string[] app = Environment.GetCommandLineArgs();
            // //LogWriter log = new LogWriter(Path.GetFileNameWithoutExtension(app[0]), exePath);
            // //log.WriteToFile(string.Format(@"Старт программы в {0}", DateTime.Now));
            // //log.WriteToFile(new string('-', 20));

            // #region Enviroment settings

            // /*Проверяем наличие каталого in, out, err. Если каталогов нет, завершаем процесс*/
            // if (!Directory.Exists(exePath + @"\in"))
            // {
            //     cMsg.WriteLine(@"Отсуствует каталог \in.", @"Error");
            //     cMsg.WriteLine(@"Создаём...", @"Message");
            //     Directory.CreateDirectory(string.Format(@"{0}\in", exePath));
            // }

            // if (!Directory.Exists(exePath + @"\out"))
            // {
            //     cMsg.WriteLine(@"Отсуствует каталог \out.", @"Error");
            //     cMsg.WriteLine(@"Создаём...", @"Message");
            //     Directory.CreateDirectory(string.Format(@"{0}\out", exePath));
            // }

            // if (!Directory.Exists(exePath + @"\err"))
            // {
            //     cMsg.WriteLine(@"Отсуствует каталог \err.", @"Error");
            //     cMsg.WriteLine(@"Создаём...", @"Message");
            //     Directory.CreateDirectory(string.Format(@"{0}\err", exePath));
            // }

            // if (!Directory.Exists(exePath + @"\dispose"))
            // {
            //     cMsg.WriteLine(@"Отсуствует каталог \dispose.", @"Error");
            //     cMsg.WriteLine(@"Создаём...", @"Message");
            //     Directory.CreateDirectory(string.Format(@"{0}\dispose", exePath));
            //  }

            // if (!Directory.Exists(exePath + @"\update"))
            // {
            //     cMsg.WriteLine(@"Отсуствует каталог \update.", @"Error");
            //     cMsg.WriteLine(@"Создаём...", @"Message");
            //     Directory.CreateDirectory(string.Format(@"{0}\update", exePath));
            // }

            // if (!Directory.Exists(exePath + @"\add"))
            // {
            //     cMsg.WriteLine(@"Отсуствует каталог \add.", @"Error");
            //     cMsg.WriteLine(@"Создаём...", @"Message");
            //     Directory.CreateDirectory(string.Format(@"{0}\add", exePath));
            // }


            // if (!Directory.Exists(exePath + @"\misc"))
            // {
            //     cMsg.WriteLine(@"Отсуствует каталог \misc.", @"Error");
            //     cMsg.WriteLine(@"Создаём...", @"Message");
            //     Directory.CreateDirectory(string.Format(@"{0}\add", exePath));
            //     cMsg.WriteLine(@"В каталоге \misc отсутствют файлы Podr.gz и Rar.exe.", @"Alert");
            //     cMsg.WriteLine(@"Добавьте эти файлы и повторите запуск.", @"Alert");
            //     Environment.Exit(0);
            // }


            // #endregion

            // #region Update checking

            // /*Проверяем наличие обновления чеккера на сервере*/
            // //log.WriteToFile(string.Format(@"Проверяем наличие обновлений чеккера: {0}", DateTime.Now));

            // var ftpAddr = new Uri(@"ftp://10.194.201.72/OUT/chkNewArv.exe");
            // //var ftpAddr = new Uri(@"ftp://10.196.192.21");

            // cMsg.WriteLine(string.Format(@"Подключаюсь к FTP-серверу {0} (МРЦ Урал)", ftpAddr.Host), @"Message");
            // FtpManager ftp = new FtpManager(@"mro75", @"ural1175");
            // var updFounded = ftp.CheckUpdate(ftpAddr.ToString());
            // /*Если оно есть, то...*/
            // if (updFounded)
            // {
            //     cMsg.WriteLine(string.Format(@"Нашёл обновление {0}. Буду обновлять...", Path.GetFileName(ftpAddr.ToString())), @"Message");
            //     /*... скачиваем файл chkNewArv.exe и обновляем его в каталоге C:\adm_test*/
            //     var loaded = ftp.DownloadFileFromFtp(ftpAddr.ToString());
            //     if (loaded)
            //     {
            //         //log.WriteToFile(string.Format(@"Скачиваем обновление чеккера: {0}", DateTime.Now));

            //         cMsg.WriteLine(@"Получено обновление с FTP-сервера", @"Message");
            //         try
            //         {
            //             File.Copy(string.Format(@"{0}\update\{1}", exePath, Path.GetFileName(ftpAddr.ToString())),
            //                 string.Format(@"C:\adm_test\chk\{0}", Path.GetFileName(ftpAddr.ToString())), true);

            //             File.Copy(string.Format(@"{0}\update\{1}", exePath, Path.GetFileName(ftpAddr.ToString())),
            //                 string.Format(@"{0}\misc\{1}", exePath, Path.GetFileName(ftpAddr.ToString())), true);

            //             /*Если получили обновление чеккера, то описываем в отчёте дату модификации полученного файла и*/
            //             /*его размер в байтах                                                                         */
            //             report.WriteToFile(@"Получено обновление программы chkNewArv.exe.");
            //             FileInfo chk = new FileInfo(string.Format(@"{0}\update\{1}", exePath,
            //                 Path.GetFileName(ftpAddr.ToString())));
            //             report.WriteToFile(string.Format(@"Дата модификации: {0}; Размер: {1} байт", chk.LastAccessTime, 
            //                 chk.Length));
            //             report.WriteToFile(new string('-', 70));
            //         }
            //         catch (Exception err)
            //         {
            //             cMsg.WriteLine(string.Format(@"Ошибка: {0}", err.Message), @"Error");
            //         }
            //     }
            // }

            // #endregion

            // #region File processing

            // /*Создаём экземпляр класса FLister для формирования списка имеющихся к обработке файлов*/
            // /*Вызываем конструктор с параметрами, передаём путь и маску для поиска                 */
            // FLister files = new FLister(exePath + @"\in", @"?75A?FD.???");

            // //log.WriteToFile(string.Format(@"Строим список файлов для обработки: {0}", DateTime.Now));

            // /*Выводим статистические данные*/
            // cMsg.WriteLine(new string('-', 60), @"Normal");
            // cMsg.WriteLine(string.Format("Файлов к обработке: {0}", files.FList.Count()), @"Confirm");
            // cMsg.WriteLine(new string('-', 60), @"Normal");

            // //report.WriteToFile(new string('-', 70));
            // report.WriteToFile(string.Format(@"ФАЙЛОВ К ОБРАБОТКЕ: {0}", files.FList.Count()));



            // int i = 1;
            // foreach (var fname in files.FList)
            // {
            //     cMsg.WriteLine(string.Format(@"{0}) {1}", i, Path.GetFileName(fname)), @"Confirm");
            //     report.WriteToFile(string.Format("\t{0}) {1}", i, Path.GetFileName(fname)));
            //     i++;
            // }


            // /*Запускаем обработку файлов*/
            //// cMsg.WriteLine(@"Работаю, ждите", @"Normal");
            // //cMsg.WriteLine(new string('-', 60), @"Normal");

            // ERemover cutter = new ERemover();
            // cutter.DeleteErrors(files.FList);
            // //log.WriteToFile(string.Format(@"Файлы обработаны: {0}", DateTime.Now));
            // #endregion

            // #region FTP uploading

            // /*Если мы запускались без параметра -noftp...*/
            // if (files.FList.Count() != 0)
            // {
            //     if (doFtp)
            //     {
            //         //log.WriteToFile(string.Format(@"Выгружаем данные на FTP: {0}", DateTime.Now));
            //         bool res;

            //         cMsg.WriteLine(new string('☺', 60), @"Alert");
            //         cMsg.WriteLine(@"Выкладываю результаты на FTP-сервер", @"Message");
            //         cMsg.WriteLine(new string('☺', 60), @"Alert");

            //         FLister uplList = new FLister(string.Format(@"{0}\out", exePath), @"?75_???.rar");

            //         i = 1;

            //        // report.WriteToFile(new string('-', 70));
            //         report.WriteToFile(string.Format(@"К ВЫГРУЗКЕ {0} АРХИВОВ.", uplList.FList.Length));


            //         foreach (string f in uplList.FList)
            //         {
            //             cMsg.WriteLine(string.Format(@"{0}) {1}", i, Path.GetFileName(f)), @"Confirm");
            //             report.WriteToFile(string.Format("\t{0}) {1}", i, Path.GetFileName(f)));
            //             i++;
            //         }
            //         report.WriteToFile(new string('-', 70));
            //         cMsg.WriteLine(new string('-', 60), @"Message");
            //         /*... и если мы запускались с параметром -upr...*/
            //         bool upload = false;
            //         if (withPrompt)
            //         {
            //             /*... спаршиваем, будем ли выгружать в МРЦ*/
            //             cMsg.Write(@"Выгружаем в МРЦ?(y\n): ", "Confirm");
            //             var ans = Console.ReadKey(false).Key;
            //             if (ans == ConsoleKey.Y)
            //             {
            //                 /*Да, выгружаем*/
            //                 cMsg.Write("\tПодтверждение.\r\n", "Confirm");
            //                 upload = true;
            //             }
            //             else if (ans == ConsoleKey.N)
            //             {
            //                 /*Нет, отказываемся (upload =  false)*/
            //                 cMsg.Write("\tОтказ.\r\n", @"Error");
            //                 report.WriteToFile(@"Выгрузка на сервер МРЦ не была подтверждена.");
            //             }
            //             else
            //             {
            //                 /*Ответ не распознан - не выгружаем*/
            //                 cMsg.Write("\tОтвет не распознан.\r\n", @"Alert");
            //                 report.WriteToFile(@"При выгрузке на сервер МРЦ получили невнятный ответ.");
            //             }
            //         }
            //         else
            //         {
            //             upload = true;
            //         }

            //         Uri uplFtpAddr;
            //         if (upload)
            //         {
            //             //uplFtpAddr = new Uri(@"ftp://10.194.201.72/in");
            //             //FtpManager uralFtp = new FtpManager(@"mro75", @"ural1175");
            //             uplFtpAddr = new Uri(@"ftp://10.196.144.200/ugai/files/roland/ural");
            //             FtpManager uralFtp = new FtpManager(@"roland", @"grOsSer1l0");

            //             cMsg.WriteLine(string.Format(@"Подключаюсь к FTP-серверу {0} (МРЦ Урал) ", uplFtpAddr.Host),
            //                 @"Message");

            //            // report.WriteToFile(new string('-', 70));
            //             report.WriteToFile(string.Format(@"НА {0} УСПЕШНО ВЫГРУЖЕНЫ:", uplFtpAddr));

            //             i = 1;
            //             foreach (string arch in uplList.FList)
            //             {
            //                 res = uralFtp.UploadFileToFtp(arch, uplFtpAddr.ToString());
            //                 if (res)
            //                 {
            //                     report.WriteToFile(string.Format("\t{0}) {1}", i, Path.GetFileName(arch)));
            //                     i++;
            //                 }
            //                 else
            //                 {
            //                     report.WriteToFile(string.Format("\tВыгрузка файла {0} потерпела неудачу.",
            //                         Path.GetFileName(arch)));
            //                 }
            //             }
            //             report.WriteToFile(new string('-', 70));
            //             //log.WriteToFile(string.Format(@" {0}", DateTime.Now));

            //             if (withPrompt)
            //                 upload = false;

            //             cMsg.WriteLine(@"Выгрузка на FTP-сервер МРЦ Урал завершена",
            //                 @"Confirm");

            //             cMsg.WriteLine(new string('-', 60), @"Message");
            //         }

            //         if (withPrompt)
            //         {
            //             /*Те же дейсвтия для ИЦ*/
            //             Console.ForegroundColor = ConsoleColor.Cyan;
            //             cMsg.Write(@"Выгружаем в ИЦ? (y\n): ", "Confirm");
            //             Console.ResetColor();
            //             var ans = Console.ReadKey(false).Key;
            //             if (ans == ConsoleKey.Y)
            //             {
            //                 cMsg.Write("\tПодтверждение.\r\n", @"Confirm");
            //                 upload = true;
            //             }
            //             else if (ans == ConsoleKey.N)
            //             {
            //                 cMsg.Write("\tОтказ.\r\n", @"Error");
            //                 report.WriteToFile(@"Выгрузка на сервер ИЦ не была подтверждена.");
            //             }
            //             else
            //             {
            //                 cMsg.Write("\tОтвет не распознан.\r\n", @"Alert");
            //                 report.WriteToFile(@"При выгрузке на сервер ИЦ получили невнятный ответ.");
            //             }
            //         }

            //         if (upload)
            //         {
            //             uplFtpAddr = new Uri(@"ftp://vc_ic.uvd.chel.su/%2fusers/obmen.zad/obmen.gai");
            //             FtpManager icFtp = new FtpManager(@"gaiobmen", @"cntggkth");

            //             //uplFtpAddr = new Uri(@"ftp://10.196.144.200/ugai/files/roland/ic");
            //             //FtpManager icFtp = new FtpManager(@"roland", @"grOsSer1l0");

            //             cMsg.WriteLine(string.Format(@"Подключаюсь к FTP-серверу {0} (ИЦ) ", uplFtpAddr.Host),
            //                 @"Message");

            //             //report.WriteToFile(new string('-', 70));
            //             report.WriteToFile(string.Format(@"НА {0} УСПЕШНО ВЫГРУЖЕНЫ:", uplFtpAddr));

            //             i = 1;
            //             foreach (string arch in uplList.FList)
            //             {
            //                 if (arch != null)
            //                 {
            //                     if (Path.GetFileName(arch).StartsWith(@"f") || Path.GetFileName(arch).StartsWith(@"a"))
            //                     {
            //                         res = icFtp.UploadFileToFtp(arch, uplFtpAddr.ToString());

            //                         if (res)
            //                         {
            //                             report.WriteToFile(string.Format("\t{0}) {1}", i, Path.GetFileName(arch)));
            //                             i++;
            //                         }
            //                         else
            //                         {
            //                             report.WriteToFile(string.Format("\tВыгрузка файла {0} потерпела неудачу.",
            //                                 Path.GetFileName(arch)));
            //                             report.WriteToFile(string.Format("\t{0}",icFtp.ErrorrMessage));
            //                         }
            //                     }
            //                 }
            //             }
            //         }
            //         cMsg.WriteLine(@"Выгрузка на FTP-сервер ИЦ завершена", @"Confirm");
            //         cMsg.WriteLine(new string('-', 60), @"Message");
            //         //report.WriteToFile(new string('-', 70));
            //     }
            //     else
            //     {

            //         cMsg.WriteLine(new string('*', 60), @"Error");
            //         cMsg.WriteLine(@"При запуске использован параметр [-noftp].", @"Message");
            //         cMsg.WriteLine(@"Выгрузка на FTP-сервера производиться не будет.", @"Message");
            //     }
            // }


            // #endregion

            // #region Cleansing \out

            // var clearList = new FLister(string.Format(@"{0}\out", exePath), @"?75_???.rar");
            // if (clearList.FList != null)
            // {
            //     foreach (var arch in clearList.FList)
            //     {
            //         var fileName = Path.GetFileName(arch);
            //         if (fileName != null && fileName.StartsWith(@"a"))
            //         {
            //             File.Move(arch, @"G:\ADM.DBF\out\COMMON\ora\arh");
            //         }
            //         else if (Path.GetFileName(arch).StartsWith(@"f"))
            //         {
            //             File.Move(arch, @"G:\amt.dbf\out\GIC1\ARCH");
            //         }
            //         else
            //         {
            //             File.Move(arch, @"G:\TASKS.EXE\VUD.EXE\FIS_VUD\arh\2017");
            //         }
            //     }
            // }

            // #endregion


            // for (int t = 0; t <= 2; t++)
            // {
            //     Console.Beep();
            // }

            // if (doWaiting)
            // {
            //     cMsg.WriteLine(@"Готово! Для завершения нажмите любую клавишу...", @"Confirm");
            //     Console.ReadKey();
            // }
            // //report.WriteToFile(new string('-', 70));
            // report.WriteToFile(new string('*', 70));
        }
    }
  }
