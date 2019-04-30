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
    }
}
