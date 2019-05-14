using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KontrolWork
{
    class Test
    {
        public int TestMethod(List<Parametr> list)
        {
            return list.FirstOrDefault().Value;
        }
    }

    class Student
    {
        public string Name { get; set; }
        public int TrueAnswer { get; set; }
    }

    class Parametr
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    class Program
    {
        Student Student { get; set; }
        string ClassName { get; set; }
        string MethodName { get; set; }
        List<Parametr> Parametrs { get; set; }

        public Program(string ClassName, string MethodName, List<Parametr> parametrs)
        {
            this.StudentName = StudentName;
            this.ClassName = ClassName;
            this.MethodName = MethodName;
            this.Parametrs = parametrs;
        }
    }
    class MainClass
    {
        public static void Main(string[] args)
        {
            //задача 1.1
            /*
            //наша дата
            var date = new DateTime();
            List<string> list = new List<string>();
            string path = @"/Users/faritsamardanov/Documents/Shamardanov_Farit_11-808/2018/FALL/KontrolWork/KontrolWork/Students";
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach(var item in dir.GetDirectories())
            {
                string temp = item.FullName;
                var dirInfo = new DirectoryInfo(temp);
                list.AddRange(dirInfo.EnumerateFiles()
                    .Where(info => info.CreationTime.Date == date)
                    .Select(info => info.FullName)
                    .ToList());
            }
            */
            string path = @"Users/faritsamardanov/Documents/Shamardanov_Farit_11-808/2018/FALL/KontrolWork/KontrolWork/Students/Root";
            DirectoryInfo dir = new DirectoryInfo(path);
            var dirs = Directory.GetDirectories(path);
            foreach(var item in dirs)
            {
                var files = Directory.GetFiles(item);
                foreach(var file in files)
                {
                    List<Student> students = new List<Student>();
                    students.Add(new St)
                }
            }
        }

        public Program Check(string str)
        {
            Student student = new Student();
            student.Name = null;
            string ClassName = null;
            string MethodName = null;
            List<Parametr> list = new List<Parametr>();
            // здесь получаем все поля прописанные выше
            if(list == Tr)
            return new Program(ClassName, MethodName, list); 
        }

        public List<int> TrueMethod()
        {
            
        }
    }
}
