using System;
using System.IO;
using System.Text;

namespace BTree
{
    class MainClass
    {
        private const int Degree = 3;
        BTree<string, string> Btree = new BTree<string, string>(Degree);

        public static void Main(string[] args)
        {
            var test = new FileGeneration();
            test.FileGen();
            var name = test.FileNameGeneration();
            test.Add(name);
            Console.WriteLine(name);
            Console.WriteLine(test.Get(name));
        }

        //public void FileGen()
        //{
        //    for (int i = 0; i < 100; i++)
        //    {
        //        string name = FileNameGeneration();
        //        string path = @"/Users/faritsamardanov/Desktop/Test/" + name;
        //        using (FileStream fs = File.Create(path))
        //        {
        //            Byte[] info = new UTF8Encoding(true).GetBytes(i.ToString());
        //            // Add some information to the file.
        //            fs.Write(info, 0, info.Length);
        //        }
        //        Btree.Insert(name, path);
        //    }
        //}

        //public string FileNameGeneration()
        //{
        //    return String.Format(@"{0}.txt", System.Guid.NewGuid());
        //}
    }
}
