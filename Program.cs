using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rainbow_School
{
    class School
    {
        private string fileName = @"D:\Rainbow_School_Data.txt";

        public School()
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
        }

        public void WriteData()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter ID of the teacher: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Name of the teacher: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Class: ");
            string clas = Console.ReadLine();
            Console.WriteLine("Enter Section: ");
            string section = Console.ReadLine();

            string con_cat = id + "\t" + name + "\t" + clas + "\t" + section;

            string myfile = fileName;
            using (StreamWriter sw = File.AppendText(myfile))
            {
                sw.WriteLine(con_cat);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ReadData()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (new FileInfo(fileName).Length == 0)
            {
                Console.WriteLine("File is Empty");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            else
            {
                StreamReader sr = new StreamReader(fileName);
                Console.WriteLine("-------------------------------");
                Console.WriteLine("ID\tName\tClass\tSection");
                Console.WriteLine("-------------------------------");
                string str = sr.ReadLine();

                while(str != null)
                {
                    Console.WriteLine(str);
                    str = sr.ReadLine();
                }

                sr.Close();
            }
            Console.ForegroundColor = ConsoleColor.White;

        }

        public void UpdateData()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (new FileInfo(fileName).Length == 0)
            {
                Console.WriteLine("File is Empty");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            else
            {
                Console.WriteLine("Enter ID of the teacher: ");
                string id = Console.ReadLine();
                bool result = false;
                StreamReader sr = new StreamReader(fileName);
                string str = sr.ReadLine();
                int line_count = 0;
                while(str != null)
                {
                    line_count++;
                    if(Regex.IsMatch(str, $@"\b{id}\b"))
                    {
                        result = true;
                        break;
                    }
                    str = sr.ReadLine();
                }
                sr.Close();

                if (result)
                {
                    Console.WriteLine("Enter Name of the teacher: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter Class: ");
                    string clas = Console.ReadLine();
                    Console.WriteLine("Enter Section: ");
                    string section = Console.ReadLine();

                    string con_cat = id + "\t" + name + "\t" + clas + "\t" + section;

                    string[] arrLine = File.ReadAllLines(fileName);
                    arrLine[line_count - 1] = con_cat;
                    File.WriteAllLines(fileName, arrLine);
                    Console.WriteLine("Details Updated Successfully!");
                }
                else
                {
                    Console.WriteLine("ID doesn't Exists!");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DeleteData()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (new FileInfo(fileName).Length == 0)
            {
                Console.WriteLine("File is Empty");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            else
            {
                Console.WriteLine("Enter ID of the teacher: ");
                string id = Console.ReadLine();
                bool result = false;
                StreamReader sr = new StreamReader(fileName);
                string str = sr.ReadLine();
                int line_count = 0;
                while (str != null)
                {
                    line_count++;
                    if (Regex.IsMatch(str, $@"\b{id}\b"))
                    {
                        result = true;
                        break;
                    }
                    str = sr.ReadLine();
                }
                sr.Close();

                if (result)
                {
                    string[] readText = File.ReadAllLines(fileName);

                    string ToBeDeleted = readText[line_count - 1];

                    File.WriteAllText(fileName, String.Empty);

                    using (StreamWriter writer = new StreamWriter(fileName))
                    {
                        foreach (string s in readText)
                        {
                            if (!s.Equals(ToBeDeleted))
                            {
                                writer.WriteLine(s);
                            }
                        }
                    }

                    Console.WriteLine("ID Deleted Successfully!");
                }
                else
                {
                    Console.WriteLine("ID doesn't Exists!");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            School obj = new School();
            int num;
            string s = "Welcome to Rainbow School!";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(s);
            Console.ForegroundColor = ConsoleColor.White;
            do
            {
                Console.WriteLine();
                Console.WriteLine("1. Add Teacher's Data");
                Console.WriteLine("2. Update Teacher's Data");
                Console.WriteLine("3. Display Teacher's Data");
                Console.WriteLine("4. Delete Teacher's Data");
                Console.WriteLine("5. Exit");
                Console.WriteLine();
                Console.WriteLine("Your Choice: ");
                Console.ForegroundColor = ConsoleColor.Green;
                num = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                switch (num)
                {
                    case 1:
                        obj.WriteData();
                        break;

                    case 2:
                        obj.UpdateData();
                        break;

                    case 3:
                        obj.ReadData();
                        break;

                    case 4:
                        obj.DeleteData();
                        break;

                    case 5: return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please Provide Valid Input");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            } while (num >= 1 && num <= 5);
        }
    }
}
