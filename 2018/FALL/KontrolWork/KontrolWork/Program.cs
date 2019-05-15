using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using.System.Threading.Tasks;
using System.Threading.Tasks;

namespace KontrolWork
{
    class MainClass
    {
        class Studend
        {
            public bool check;
            public List<Execute> Methods;
            public string Path;
            public Studend(string path)
            {
                Path = path;
                ParseStudent(path);
            }
            public void ParseStudent(string path)
            {
                Methods = Directory.GetFiles(path).Select(f => File.ReadAllLines(f))
               .Select(t =>
               {
                   return new Execute(t[1], t[0], t[2], t[3]);
               }).ToList();
            }
        }

        class Execute
        {
            public bool check;
            public string MethodName;
            public string ClassName;

            public string arguments;
            public string response;
            public Execute(string methodName, string className, string args, string response)
            {
                this.MethodName = methodName;
                this.ClassName = className;
                this.arguments = args;
                this.response = response;
            }//type {value} responseType {value}
            static object[] getArgs(string text)
            {
                var mathes = new Regex(@"{+\w*}+").Matches(text);
                var result = new List<object>();
                foreach (Match e in mathes)
                {
                    result.Add(e.Value.Substring(1, e.Value.Length - 2));
                }
                return result.ToArray();
            }

            public async void CheckMethod()
            {
                var type = Type.GetType(ClassName);
                var method  = type.GetMethod(MethodName);
                var constructor = type.GetConstructor(new Type[] { });
                var result = constructor.Invoke(null);
                var args = getArgs(arguments); 

                var res = method.Invoke((object)result, args);
                var responseType = Type.GetType(response.Split('{','}')[0]);
                var responseValue = int.Parse(response.Split('{', '}')[1]);
                check = (int)res == responseValue;
            }
        }

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
            var students = Directory.GetDirectories(@"путь до файла").Select(f => new Studend(f)).ToList();
            foreach(var student in students)
            {
                foreach(var e in student.Methods)
                {
                    Task.Run(() => e.CheckMethod());
                }
            }
            //вывод таблицы
            var arrayTrueAnswer = students.Select(s => s.Methods.Where(f => f.check).Count()).ToArray();
            var i = 0;
            foreach(var student in students)
            {
                Console.WriteLine(student.Path + ": " + arrayTrueAnswer[i]);
                i++;
            }
        }

    }
}
