using System;
using System.IO;
using System.Text;

namespace BTree
{
    //задаяа реализует индексацию файлов для быстрого доступа к ним
    public class FileGeneration
    {
        private const int Degree = 3;
        BTree<string, string> Btree = new BTree<string, string>(Degree);
        public void FileGen()
        {
            for (int i = 0; i < 100; i++)
            {
                string name = FileNameGeneration();
                string path = @"/Users/faritsamardanov/Desktop/Test/" + name;
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(i.ToString());
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                Btree.Add(name, path);
            }
        }

        public string Get(string fileName)
        {
            return Btree.Search(fileName).Pointer;
        }

        public void Add(string fileName)
        {
            string name = fileName;
            string path = @"/Users/faritsamardanov/Desktop/Test/" + name;
            using (FileStream fs = File.Create(path))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(name);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
            Btree.Add(name, path);
        }

        public void Delete(string fileName)
        {
            Btree.Delete(fileName);
        }
        //генерация имени файла
        public string FileNameGeneration()
        {
            return String.Format(@"{0}.txt", System.Guid.NewGuid());
        }
    }
}
