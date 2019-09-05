using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileCabinet.Database.ADO.Repositories;
using FileCabinet.Database.Core.Models;

namespace FileCabinet.UI
{
    class Program
    {
        static CabinetRepository cabinet = new CabinetRepository();

        static void Main(string[] args)
        {
            Menu();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        static void Menu()
        {
            Console.WriteLine("************************************************************");
            Console.WriteLine("Enter number, that you want to do:");
            Console.WriteLine("1 - Show all");
            Console.WriteLine("2 - Show one by Id");
            Console.WriteLine("3 - Create new");
            Console.WriteLine("4 - Update");
            Console.WriteLine("5 - Delete");
            Console.WriteLine("6 - Exit");
            string s = Console.ReadLine();

            switch (s)
            {
                case "1":
                    SelectAll();
                    break;
                case "2":
                    SelectById();
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    break;
                default:
                    Console.WriteLine("Entered wrong key, repeat please");
                    Menu();
                    break;
            }
        }

        static void SelectAll()
        {
            List<Cabinet> list = cabinet.GetAll();
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
            Menu();
        }

        static void SelectById()
        {
            Console.WriteLine("Enter Id:");
            string s = Console.ReadLine();
            int id = 0;
            if (int.TryParse(s, out id))
            {
                var t = cabinet.GetById(id);
                if (t != null)
                    Console.WriteLine(t.ToString());
                else
                    Console.WriteLine("Information does not exist");
            }
            Menu();
        }
    }
}
