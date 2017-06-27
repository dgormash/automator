using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AutomatorPrg
{
    internal class ERemover
    {
        // ReSharper disable once FunctionComplexityOverflow
        public void DeleteErrors(string[] arrFileList)
        {
            string lineNum = string.Empty;
            string errField = string.Empty;
            string path = Directory.GetCurrentDirectory();
            string addName = null;

            ReportWriter report = new ReportWriter(DateTime.Now.ToString("ddMMyyyy"), path);
            /*Для всех файлов во входном списке arrFileList...*/
            int i = 1;

            //report.WriteToFile(new string('-', 70));
            report.WriteToFile(@"СОЗДАННЫЕ ОТЧЁТЫ ОБ ОШИБКАХ:");
            ConsoleMessage cMsg = new ConsoleMessage();
            foreach (string f in arrFileList)
            {
                var name = Path.GetFileName(f);
                cMsg.WriteLine(new string('-', 60), @"Normal");
                cMsg.WriteLine(string.Format("{0}) Обрабатываем файл {1}", i, name), @"Confirm");
                bool doCycle = true;
                while (doCycle)
                {
                    /*... проверяем, есть ли файл с таким же именем, и добавочным расширением .txt*/
                    if (File.Exists(f + @".txt"))
                    {
                        cMsg.WriteLine(string.Format(@"Нашёл файл ошибок {0}.txt... Буду работать", name), @"Alert");

                        /*Если есть, то в каталоге \err создаём файл с именем исходного файла и     */
                        /*добавочным расширением .err. Это будет файл, содержащий вырезанные ошибки*/
 /*1*/                       FileStream newFile = File.Create(path + @"\err\" + name + @".err");
                        report.WriteToFile(string.Format("\t{0}.err", name));
                        newFile.Dispose();

                        /*Загружаем содержимое ихсодного  файла в список (initFile)*/
                        StreamReader fileContent = new StreamReader(f, Encoding.GetEncoding(866));
                        string srcStr;
                        List<string> initFile = new List<string>();
                        while ((srcStr = fileContent.ReadLine()) != null)
                        {
                            initFile.Add(srcStr);
                        }
                        /*Освобождаем исходный файл*/
                        fileContent.Dispose();

                        /*Предварительно очищаем каталог от файла с таким же именем, чтобы не вылететь в ошибку*/
                        if (File.Exists(string.Format(@"{0}\dispose\{1}", path, name)))
                        {
                            File.Delete(string.Format(@"{0}\dispose\{1}", path, name));
                        }

                        /*Перемещаем исходный файл в каталог \dispose, чтобы больше он нам не мешал*/
                        /*В дальнейшем будем работать с файлом, который получится в итоге          */
                        File.Move(f, string.Format(@"{0}\dispose\{1}", path, name));

                        /*Читаем в цикле строки файла ошибок и от каждой строки выделяем номер (lineNum)   */
                        /*ошибочной строки (lineNum) и аттрибут поля, в которм содержится ошибка (errField)*/
                        StreamReader errContent = new StreamReader(f + @".txt", Encoding.GetEncoding(866));


                        string errStr;
                        while ((errStr = errContent.ReadLine()) != null)
                        {
                            /*Если сторка пустая, выходим из цикла (на всякий пожарный)*/
                            if (errStr == " ")
                            {
                                break;
                            }

                            /*Получаем номер строки исходного файла, в котором содержится ошибка*/
                            foreach (Match m in Regex.Matches(errStr, @"(^[0-9]+?):"))
                            {
                                lineNum = m.Groups[1].Value; //Получили номер строки с ошибкой
                            }

                            /*Получаем аттрибут поля, в котором содержится ошибка*/
                            //foreach (Match m in Regex.Matches(errStr, @"/([0-9]{2})[:|=]"))
                            foreach (Match m in Regex.Matches(errStr, @"/([0-9]{2}(.+?))$"))
                            {
                                errField = m.Groups[1].Value;
                            }

                            /*Формируем выходную строку, которую будем записывать в файл ошибок каталога \err*/
                            /*Строка содержит номер строки с ошибкой, аттрибут, исходный текст*/
                            string lineOut = String.Format("/{0} ;{1}", /*errStr,*/
                                errField,
                                initFile[Convert.ToInt32(lineNum) - 1]);

                            /*Производим запись в файл ошибок каталога err*/
                            using (
                                StreamWriter errWriter =
                                    new StreamWriter(path + @"\err\" + name + @".err", true,
                                        Encoding.GetEncoding(866)))
                            {
                                errWriter.WriteLine(lineOut);
                                //errCnt++; //Увеличиваем счётчик отработанныйх ошибок на единицу
                            }
                            /*В списки initFile соттветствующую строку помечаем специальным маркером*/
                            initFile[Convert.ToInt32(lineNum) - 1] = @"err - |" +
                                                                     initFile[Convert.ToInt32(lineNum) - 1];
                        }

                        /*Освобождаем файл с ошибками*/
                        errContent.Dispose();

                        if (File.Exists(string.Format(@"{0}\dispose\{1}.txt", path, name)))
                        {
                            File.Delete(string.Format(@"{0}\dispose\{1}.txt", path, name));
                        }
                        
                        /*Переносим файл с ошибками в каталог \dispose, дабы избежать бесконечного цикла*/
                        File.Move(string.Format(@"{0}.txt", f), string.Format(@"{0}\dispose\{1}.txt", path, name));

                        /*Создаём итоговый файл с исходным именем. Подразумеваем, что файл ошибок был*/
                        FileStream errOut = File.Create(f);
                        errOut.Dispose();
                      
                        /*Теперь обрабатываем список initFile, в котором содержатся помеченные маркером строки*/
                        /*Маркер err - добавляется вначале строки.                                            */
                        Regex rgx = new Regex(@"^err - \|");

                        /*Фромируем новый файл с исходным именем, исключая строки, помеченные маркером err -*/
                        foreach (string str in initFile)
                        {
                            if (!rgx.IsMatch(str))
                            {
                                using (
                                    StreamWriter outWriter =
                                        new StreamWriter(f, true,
                                            Encoding.GetEncoding(866)))
                                {
                                    outWriter.WriteLine(str);
                                }
                                //outCnt ++;
                            }
                        }

                        cMsg.WriteLine(string.Format(@"Ошибки в файле {0} удалены", name), @"Confirm");

                        /*Ищем, есть ли в каталоге \add файл с таким же именем как и исходный, но расширением .000 */
                        /*Это файл добавлений. Будем  обрабатывать и удалять, таким образом                        */
                        /*Добавка будет производиться в первый\единственный файл отгрузки                          */
                        if (File.Exists(string.Format(@"{0}\add\{1}.000", path, Path.GetFileNameWithoutExtension(name))))
                        {
                            new FileAppenderOld().AppendFile(f, string.Format(@"{0}\add\{1}.000", path, 
                                Path.GetFileNameWithoutExtension(name)));
                            addName = Path.GetFileName(name);
                        }
                        /*Запускаю проверку файла в текущем каталоге чеккером chkNewArv.exe*/
                        new CheckStarter().StartChekcer(f);

                        /*Проверяем, если появился файл архива, то завершаем обработку текущего файла и*/
                        /*переходим к следующему в списке, если он есть                                */

                        string[] arcs = Directory.GetFiles(string.Format(@"{0}\in", path), @"?75_???.rar");

                        /*Сканируем рабочий каталог и ищем там запакованный архив. */
                        foreach (string arc in arcs)
                        {
                            //Добавить возможност завершить цикл по наличию запакованного архива
                            if (File.Exists(arc))
                            {
                                if (File.Exists(string.Format(@"{0}\out\{1}", path, Path.GetFileName(arc))))
                                {
                                    File.Delete(string.Format(@"{0}\out\{1}", path, Path.GetFileName(arc)));
                                }
                                File.Move(arc, string.Format(@"{0}\out\{1}", path, Path.GetFileName(arc)));

                                cMsg.WriteLine(string.Format(@"Файл {0} успешно запакован", name), @"Confirm");
                                doCycle = false;
                            }
                        }
                        
                    }
                    else //Файла ошибок не существует
                    {
                        /*Ищем, есть ли в каталоге \add файл с таким же именем как и исходный, но расширением .000 */
                        /*Это файл добавлений. Будем  обрабатывать и удалять, таким образом                        */
                        /*Добавка будет производиться в первый\единственный файл отгрузки                          */
                        if (File.Exists(string.Format(@"{0}\add\{1}.000", path, Path.GetFileNameWithoutExtension(name))))
                        {
                            new FileAppenderOld().AppendFile(f, string.Format(@"{0}\add\{1}.000", path,
                                Path.GetFileNameWithoutExtension(name)));
                            addName = Path.GetFileName(name);
                            
                        }

                        new CheckStarter().StartChekcer(f);

                        /*Проверяем, если появился файл архива, то завершаем обработку текущего файла и*/
                        /*переходим к следующему в списке, если он есть                                */
                        string[] arcs = Directory.GetFiles(string.Format(@"{0}\in", path), @"?75_???.rar");

                        /*Сканируем рабочий каталог и ищем там запакованный архив. */
                        foreach (string arc in arcs)
                        {
                            //Добавить возможност завершить цикл по наличию запакованного архива
                            if (File.Exists(arc))
                            {
                                if (File.Exists(string.Format(@"{0}\out\{1}", path, Path.GetFileName(arc))))
                                {
                                    File.Delete(string.Format(@"{0}\out\{1}", path, Path.GetFileName(arc)));
                                }
                                File.Move(arc, string.Format(@"{0}\out\{1}", path, Path.GetFileName(arc)));

                                cMsg.WriteLine(string.Format(@"Файл {0} успешно запакован", name), @"Confirm");
                                doCycle = false;
                            }
                            else
                            {
                                doCycle = true;
                            }
                        }                      
                    }
                }
                i++;
            }

            report.WriteToFile(new string('-', 70));
            if (addName != null)
            {
                report.WriteToFile(string.Format(@"В файл {0} добавлено содержимое {1}.000", addName,
                                Path.GetFileNameWithoutExtension(addName)));
                report.WriteToFile(new string('-', 70));
            }   
        }
    }
}