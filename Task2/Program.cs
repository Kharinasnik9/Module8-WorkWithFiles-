﻿using System;
using System.IO;
namespace Task2
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к папке, у которой нужно узнать размер:");
            string FolderPath = Console.ReadLine();

            try
            {
                long FolderSize = GetFolderSize(FolderPath);
                Console.WriteLine($"Размер папки {FolderPath}: {FolderSize} байт!");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Папка не найдена: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"У вас нет прав для работы с папками по данному пути: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Во время выполнения возникла ошибка: {ex.Message}");
            }
        }

        private static long GetFolderSize(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                throw new DirectoryNotFoundException("Данная папка не найдена");
            }

            long FolderSize = 0;
            var DirectoryInfo = new DirectoryInfo(FolderPath);

            foreach (var File in DirectoryInfo.GetFiles())
            {
                try
                {
                    FolderSize += File.Length;
                    File.Delete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Не удалось посчитать размер файла: {File.FullName}:{ex.Message}");
                }
            }

            foreach (var Folder in DirectoryInfo.GetDirectories())
            {
                try
                {
                    FolderSize += GetFolderSize(Folder.FullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не удалось получить размер папки {Folder.FullName}: {ex.Message}");
                }
            }

            return FolderSize;
        }
    }
}
