using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        static void WriteNewCabinet()
        {
            string patternForNames = @"^[а-яА-ЯёЁa-zA-Z]+$";
            string patternForDate = @"[0-9]{4}-(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])";

            Console.WriteLine("Write first name for new cabinet:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Write last name for new cabinet:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Write date birth in format YYYY-MM-DD:");
            string date = Console.ReadLine();

            if (Regex.IsMatch(firstName,patternForNames,RegexOptions.IgnoreCase)&&
                Regex.IsMatch(lastName,patternForNames,RegexOptions.IgnoreCase)&&
                Regex.IsMatch(date, patternForDate))
            {
                cabine
            }


        }
    }
}
