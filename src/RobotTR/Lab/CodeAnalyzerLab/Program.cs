using System;
using System.Collections.Generic;
using System.IO;

namespace CodeAnalyzerLab
{
    class Program
    {
        static void Main(string[] args)
        {   
            var files = new List<string>();
            GetCodeFiles("C:\\Studies\\desenvolvedor.io\\NerdStoreEnterprise", ref files);

            int fileCounter = 1;
            foreach (var file in files)
            {
                Console.WriteLine($"File number {fileCounter} | Directory: {file}");
                Console.WriteLine("-----------------------------------------------");
                ReadFile(file);
                Console.WriteLine("-----------------------------------------------");
                fileCounter++;
            }
        }

        private static void GetCodeFiles(string rootDirectory, ref List<string> filesPaths)
        {
            var files = Directory.GetFiles(rootDirectory);

            foreach (var file in files)
            {
                if (file.EndsWith(".cs"))
                    filesPaths.Add(file);
            }

            var folders = Directory.GetDirectories(rootDirectory);

            foreach (var folder in folders)
            {
                if(!folder.EndsWith("obj"))
                    GetCodeFiles(folder, ref filesPaths);
            }
        }

        private static void ReadFile(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                Console.WriteLine(sr.ReadToEnd());
            }
        }
    }
}