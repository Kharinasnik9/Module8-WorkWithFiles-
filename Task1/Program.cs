using System;
using System.IO;
namespace Task1 { 

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к папке, которую хотите очистить:");
            string FolderPath = Console.ReadLine();

            try
            {
                CleanFolder(FolderPath);
                Console.WriteLine("Очистка папки от файлов завершена!");
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

        private static void CleanFolder(string FolderPath)
        {           
            if (!Directory.Exists(FolderPath)) 
            { 
                throw new DirectoryNotFoundException("Данная папка не найдена");
            }

            TimeSpan TimeHold = TimeSpan.FromMinutes(30);
            DateTime TimeLife = DateTime.Now.Subtract(TimeHold);
            var DirectoryInfo = new DirectoryInfo(FolderPath);

            foreach (var File in DirectoryInfo.GetFiles()) 
            {
                try
                {
                    if (File.LastAccessTime < TimeLife)
                    {
                        File.Delete();
                        Console.WriteLine($"Удален файл: {File.FullName} ");
                    }
                }
                catch (Exception ex)
                { 
                    Console.WriteLine($" Не удалось удалить файл: {File.FullName}:{ex.Message}");
                }
            }

            foreach (var Folder in DirectoryInfo.GetDirectories())
            {
                try
                {
                    CleanFolder(Folder.FullName);

                    if (Folder.GetFiles().Length == 0 && Folder.GetDirectories().Length == 0)
                    {
                        Folder.Delete();
                        Console.WriteLine($"Удалена пустая папка {Folder.FullName}");
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine($"Не удалось удалить папку {Folder.FullName}: {ex.Message}");
                }
            }
        }
    }
}