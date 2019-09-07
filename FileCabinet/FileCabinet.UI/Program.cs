using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FileCabinet.Database.ADO.Repositories;
using FileCabinet.Database.Core.Models;
using FileCabinet.Database.Core.Interfaces;
using FileCabinet.Storage.Csv.Export;
using FileCabinet.Storage.Xml.Export;


namespace FileCabinet.UI
{
    class Program
    {
        static IRepository<Cabinet> cabinet = new CabinetRepository();

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
            Console.WriteLine("6 - Purge");
            Console.WriteLine("7 - Export in csv");
            Console.WriteLine("8 - Export in xml");

            Console.WriteLine("9 - Exit");
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
                    WriteNewCabinet();
                    break;
                case "4":
                    UpdateCabinet();
                    break;
                case "5":
                    DeleteCabinet();
                    break;
                case "6":
                    DeleteAllCabinets();
                    break;
                case "7":
                    ExportToCsv();
                    break;
                case "8":
                    ExportToXml();
                    break;
                case "9":
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
                cabinet.Create(new Cabinet(0, firstName, lastName, DateTime.Parse(date)));
                Console.WriteLine();
                Console.WriteLine("New cabinet is create");
                Console.WriteLine();
                Menu();
            }
            else
            {
                Console.WriteLine("Somthing wrong, try again");
                WriteNewCabinet();
            }
        }

        static void UpdateCabinet()
        {
            string patternForNames = @"^[а-яА-ЯёЁa-zA-Z]+$";
            string patternForDate = @"[0-9]{4}-(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])";

            List<Cabinet> list = cabinet.GetAll();
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("Write id for update cabinet:");
            string id = Console.ReadLine();
            int idForRecord;
            Console.WriteLine("Write first name for update cabinet:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Write last name for update cabinet:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Write date birth in format YYYY-MM-DD:");
            string date = Console.ReadLine();

            if (Regex.IsMatch(firstName, patternForNames, RegexOptions.IgnoreCase) &&
                Regex.IsMatch(lastName, patternForNames, RegexOptions.IgnoreCase) &&
                Regex.IsMatch(date, patternForDate)&&
                int.TryParse(id, out idForRecord))
            {
                cabinet.Update(new Cabinet(idForRecord, firstName, lastName, DateTime.Parse(date)));
                Console.WriteLine();
                Console.WriteLine("Cabinet is update");
                Console.WriteLine();
                Menu();
            }
            else
            {
                Console.WriteLine("Somthing wrong, try again");
                UpdateCabinet();
            }
        }

        static void DeleteCabinet()
        {
            List<Cabinet> list = cabinet.GetAll();
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("Choose which cabinet do you want to delete and enter it ID");
            string stringId = Console.ReadLine();
            int id;

            if (int.TryParse(stringId, out id))
            {
                if (cabinet.Delete(new Cabinet(id, "", "", DateTime.Now.Date)))
                    Console.WriteLine("Deleting is success");
            }
            else
            {
                Console.WriteLine("Somthing wrong, try again");
                DeleteCabinet();
            }
        }

        static void DeleteAllCabinets()
        {
            List<Cabinet> list = cabinet.GetAll();
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("Do you want delete all records? y/n");
            string choose = Console.ReadLine();

            if (choose.ToLower().Replace(" ",string.Empty) =="y")
            {
                foreach(var item in list)
                    cabinet.Delete(new Cabinet(item.Id, "", "", DateTime.Now.Date));
                Console.WriteLine("Deleting is success");
            }
            else if(choose.ToLower().Replace(" ", string.Empty) == "n")
            {
                Menu();
            }       
            else
            {
                Console.WriteLine("Somthing wrong, try again");
                DeleteAllCabinets();
            }
        }

        static void ExportToCsv()
        {

            IExport<Cabinet> exportCsv = new Storage.Csv.Export.ExportCabinet("file.csv");
            exportCsv.SaveAll(cabinet.GetAll());

            Console.WriteLine("Exporting is success");
            Menu();
        }
        static void ExportToXml()
        {

            IExport<Cabinet> exportCsv = new Storage.Xml.Export.ExportCabinet("file.xml");
            exportCsv.SaveAll(cabinet.GetAll());

            Console.WriteLine("Exporting is success");
            Menu();
        }



    }
}
