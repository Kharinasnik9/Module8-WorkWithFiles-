using System;
using System.Collections.Generic;
using System.IO;
namespace Task4

{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к бинарному файлу:");

            string FilePath = Console.ReadLine();
            string StudentsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Students");

            Directory.CreateDirectory(StudentsDirectory);

            List<Student> Students = ReadFile(FilePath); 

            Dictionary<string, List<Student>> Group2Students = new Dictionary<string, List<Student>>();

            foreach (Student student in Students)
            {
                if (!Group2Students.ContainsKey(student.Group))
                {
                    Group2Students[student.Group] = new List<Student>();
                }

                Group2Students[student.Group].Add(student);
            }

            foreach (var Group in Group2Students)
            {
                string GroupPath = Path.Combine(StudentsDirectory, $" {Group.Key}.txt");

                using (StreamWriter writer = new StreamWriter(GroupPath))
                {
                    foreach (var Student in Group.Value)
                    { 
                    writer.WriteLine($"{Student.Name}, {Student.DateOfBirth.ToShortDateString()}, {Student.AverageGrade}");
                    }
                }
            }
        }

        static List<Student> ReadFile(string FilePath)
        {
            List<Student> Students = new List<Student>();

            using (BinaryReader Reader = new BinaryReader(File.Open(FilePath, FileMode.Open)))
            {
                while (Reader.PeekChar() > -1)
                {
                    Student Student = new Student()
                    {
                        Name = Reader.ReadString(),
                        Group = Reader.ReadString(),
                        DateOfBirth = DateTime.FromBinary(Reader.ReadInt64()),
                        AverageGrade = Reader.ReadDecimal()
                    };
                    Students.Add(Student);
                }
                return Students;
            }
        }
    }
}
