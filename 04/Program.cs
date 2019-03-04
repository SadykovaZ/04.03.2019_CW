using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _04._03._2019_CW;
using System.IO;

namespace _04._03._2019_CW

{
    public struct State
    {
        public string name;
        public string capital;
        public int area;
        public double people;

        public State(string name, string capital, int area, double people)
        {
            this.name = name;
            this.capital = capital;
            this.area = area;
            this.people = people;
        }
    }

    public struct Tmp
    {
        public char find { get; set; }
        public char replace { get; set; }

    }
    class Program
    {
        static string pathFrom;
        static string pathTo;

        static void Main(string[] args)
        {

            // Ex101(FileType.sales);

            //Ex105();
            
            Console.WriteLine("Enter path From: ");
            pathFrom = @"C:\Users\СадыковаЖ\Desktop\Файловая система\TNSShort";//Console.ReadLine();
            Console.WriteLine("Enter path To: ");
            pathTo = @"C:\Users\СадыковаЖ\Desktop\Файловая система\TNSShort\001";// Console.ReadLine();

            DirectoryInfo dirPathFrom = new DirectoryInfo(pathFrom);
            DirectoryInfo dirPathTo = new DirectoryInfo(pathTo);

            if (!dirPathFrom.Exists || !dirPathTo.Exists)
            {
                throw new Exception("Directory does not exist");
            }


            Dictionary<string, bool> formats = new Dictionary<string, bool>();
            Dictionary<string, bool> formatsChoice = new Dictionary<string, bool>();

            foreach (FileInfo file in dirPathFrom.GetFiles())
            {
                if (!formats.ContainsKey(file.Extension))
                    formats.Add(file.Extension, false);
            }

            foreach (var item in formats)
            {
                string gg = item.Key;
                Console.Write(item.Key + " использовать? д/н: ");
                if (Console.ReadLine() == "д")
                    formatsChoice.Add(item.Key, true);
                Console.WriteLine("");
            }
            Console.Clear();
            foreach (var item in formatsChoice)
            {
                foreach (FileInfo file in dirPathFrom.GetFiles("*" + item.Key))
                {
                    Console.WriteLine("--> {0}", file.Name);
                }
                Console.WriteLine("-------------------------");
                Console.WriteLine("Total: {0} files", dirPathFrom.GetFiles("*" + item.Key).Length);
            }
            List<Tmp> tmp = new List<Tmp>();
            Console.WriteLine("Редактировать? д/н");
            if (Console.ReadLine() == "д")
            {
                foreach (var item in formatsChoice)
                {
                    foreach (FileInfo file in dirPathFrom.GetFiles("*" + item.Key))
                    {
                        if (LCh(file.Name).Count > 0)
                        {
                            tmp.AddRange(LCh(file.Name.Substring(0,file.Name.LastIndexOf("."))));
                        }
                    }
                    tmp = tmp.GroupBy(w => w.find).Select(s => new Tmp { find = s.Key }).ToList();

                    
                }
                foreach (Tmp item in tmp)
                {
                    Console.WriteLine(item.find + "-");
                    item.replace = !string.IsNullOrWhiteSpace(Console.ReadLine())?new char(): Console.ReadLine()[0];
                }
                foreach (var item in formatsChoice)
                {
                    foreach (FileInfo file in dirPathFrom.GetFiles("*" + item.Key))
                    {
                        string str = file.Name;
                        foreach (Tmp i in tmp)
                        {
                            str.Replace(i.find, i.replace);
                        }
                        string newP = Path.Combine(pathTo, str+""+file.Extension);
                        file.MoveTo(newP);
                    }
                   


                }
            }
        }


        public static List<Tmp> LCh(string s)
        {
            List<Tmp> res = new List<Tmp>();
            foreach (char item in s)
            {
                if((int)item>=33 && (int)item<=47)
                {
                    res.Add(new Tmp() { find = item });
                }
            }
            return res;
        }

        public enum FileType { sales, stored }
        static void Ex101(FileType ft)
        {

            FileInfo f = new FileInfo("testFile.txt");
            if (f.Exists)
            {


            }
            else
            {
                FileStream fs = f.Create();
                fs.Close();
                using (FileStream fs2 = f.Create())
                {

                }
            }


            //FileAccess - используется для определения чтения/записи лежащего в основе потока
            //FileShare - указывает как может быть разделен с другими файловыми потоками
            string path = Path.Combine("Upload", DateTime.Now.ToShortDateString(), ft.ToString(), "file.txt");
            using (FileStream FS = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {

            }
        }

        static void Ex102()
        {
            //привязываемся к директории Upload
            DirectoryInfo dir = new DirectoryInfo(@"Upload");

            dir.Create();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Полный путь: {0}", dir.FullName);
            Console.WriteLine("Название папки: {0}", dir.Name);
            Console.WriteLine("Родительский каталог: {0}", dir.Parent);
            Console.WriteLine("Время создания: {0}", dir.CreationTime);
            Console.WriteLine("Атрибуты: {0}", dir.Attributes);
            Console.WriteLine("Корневой каталог: {0}", dir.Root);


            dir.CreateSubdirectory(DateTime.Now.ToShortDateString());
            dir = new DirectoryInfo(@"C:\Users\СадыковаЖ\source\repos");
            foreach (FileInfo file in dir.GetFiles())
            {
                Console.WriteLine("\n-------------------------------\n");
                Console.WriteLine("Имя файла: {0}", file.Name);
                Console.WriteLine("Размер файла: {0}", file.Length);
                Console.WriteLine("Время создания файла: {0}", file.CreationTime);

            }



        }

        static void Ex103()
        {
            string path = @"C:\Users\СадыковаЖ\source\repos\1.txt";
            try
            {
                //считывание по строчно
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
                Console.WriteLine("*************************************************************************");
                //считывание поблочно
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    char[] arr = new char[4];
                    sr.Read(arr, 0, 4);
                    Console.WriteLine(arr);

                    Console.WriteLine(sr.ReadToEnd());
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        static void Ex104()
        {
            string path = @"C:\Users\СадыковаЖ\source\repos\1.txt";

            string text = "";
            try
            {
                //дозапись в файл
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    sw.WriteLine("Hello my friwnd");
                    sw.Write("5.5");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        static void Ex105()
        {
            State[] states = new State[2];
            states[0] = new State("Казахстан", "Астана", 200000, 80.8);
            states[1] = new State("Узбекистан", "Ташкент", 20000, 90.8);

            string path = @"C:\Users\СадыковаЖ\source\repos\states.txt";

            try
            {
                using (BinaryWriter wr = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                {
                    foreach (var item in states)
                    {
                        wr.Write(item.name);
                        wr.Write(item.capital);
                        wr.Write(item.area);
                        wr.Write(item.people);

                    }

                }
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        string capital = reader.ReadString();
                        int area = reader.ReadInt32();
                        double people = reader.ReadDouble();

                        Console.WriteLine("Страна: {0} ", name);
                        Console.WriteLine("Столица: {0} ", capital);
                        Console.WriteLine("Площадь: {0} ", area);
                        Console.WriteLine("Население: {0} ", people);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
