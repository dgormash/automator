using System;
using System.IO;

namespace AutomatorPrg
{
    class FLister
    {
        private string[] _fList;

        /*Конструктор по умолчанию*/
        public FLister()
        {
            var srcPath = Directory.GetCurrentDirectory() + @"\in"; //по умолчанию каталог in в папке с программой
            const string fMask = @"?75A?FD.???";
            MakeFList(srcPath, fMask);
        }

        /*Пользовательский конструктор, принимает путь в качестве параметра*/
        public FLister(string srcPath, string fMask)
        {
            MakeFList(srcPath, fMask);
        }

        /*Свойство FList для доступа к содержимому _fList*/
        public string[] FList
        {
            get { return _fList; }
        }

        /*Метод MakeFList загружает список файлов с путями в массив _fList*/
        private void MakeFList(string folder, string mask)
        {
            _fList = Directory.GetFiles(folder, mask, SearchOption.TopDirectoryOnly);
            Array.Sort(_fList);
        }
    }
}
