using System;
using System.IO;

namespace NameReplace
{
    class Program
    {
        static void Main(string[] args)
        {
            string _condition = "#";
            string _rootPath = @$"{Directory.GetParent(Directory.GetCurrentDirectory())}";
            
            string[] dirs = Directory.GetDirectories(_rootPath, "*", SearchOption.AllDirectories);
            string currentPath;
            var allFiles = Directory.GetFiles(_rootPath, "*", SearchOption.AllDirectories);
            bool conditionExists = false;
            var files = Directory.GetFiles(_rootPath, "*", SearchOption.TopDirectoryOnly);

            //checked all files for condition
            foreach (var fileCheck in allFiles)
            {
                if (Path.GetFileName(fileCheck).Contains(_condition))
                {
                    conditionExists = true;
                }
            }

            if (conditionExists)
            {
                Console.WriteLine($"Filenames were found containing '{_condition}'");
                //getFiles root folder and files`
                GetFiles(_rootPath, files, _condition);
                //iterate through all subfolders and it's files'
                foreach (string dir in dirs)
                {
                    currentPath = dir;
                    files = Directory.GetFiles(dir, "*", SearchOption.TopDirectoryOnly);
                    GetFiles(dir, files, _condition);
                }
            }else
                Console.WriteLine($"No filenames found containing '{_condition}'");

            Console.ReadLine();
        }

        //get current folders files
        private static void GetFiles(string dir, string[] files, string condition)
        {
            foreach (var file in files)
            {
                var info = new FileInfo(file);

                if (Path.GetFileName(file).Contains(condition))
                {
                    Console.WriteLine($"RENAMING File: {Path.GetFileNameWithoutExtension(file)} >>>> {Path.GetFileNameWithoutExtension(file).Replace(condition, "").Trim()}");
                    info.MoveTo(Path.GetFullPath(file).Replace(condition, "").Trim());
                }
            }
        }
    }
}
