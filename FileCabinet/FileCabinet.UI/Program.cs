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
        static CabinetRepository cabinet = new CabinetRepository();

        static void Main(string[] args)
        {
            Menu();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        /// <summary>
        /// menu intarface
        /// </summary>
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
            Console.WriteLine("9 - Find string in all fields");
            Console.WriteLine("10 - Import from csv");
            Console.WriteLine("11 - Inport from xml");
            Console.WriteLine("12 - Exit");
            string s = Console.ReadLine();
            Console.WriteLine();

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
                    Find();
                    break;
                case "10":
                    ImportFromCsv();
                    break;
                case "11":
                    ImportFromXML();
                    break;
                case "12":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Entered wrong key, repeat please");
                    Menu();
                    break;
            }
        }

        /// <summary>
        /// interface for find
        /// </summary>
        static void Find()
        {
            Console.WriteLine();
            Console.WriteLine("write what you want to find:");
            string s = Console.ReadLine();
            List<Cabinet> cabinets = cabinet.Find(s);
            Console.WriteLine($"Find {cabinets.Count} records");
            foreach (var item in cabinets)
                Console.WriteLine(item.ToString());
            Console.WriteLine();
            Menu();
        }

        /// <summary>
        /// interface for all records
        /// </summary>
        static void SelectAll()
        {
            Console.WriteLine();
            List<Cabinet> list = cabinet.GetAll();
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            Menu();
        }

        /// <summary>
        /// interface for one record
        /// </summary>
        static void SelectById()
        {
            Console.WriteLine();
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
            Console.WriteLine();
            Menu();
        }

        /// <summary>
        /// interface for creating new record
        /// </summary>
        static void WriteNewCabinet()
        {
            string patternForNames = @"^[а-яА-ЯёЁa-zA-Z]+$";
            string patternForDate = @"[0-9]{4}-(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])";

            Console.WriteLine();
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
                Console.WriteLine();
                Menu();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Somthing wrong, try again");
                Console.WriteLine();
                WriteNewCabinet();
            }
        }

        /// <summary>
        /// interface for update record
        /// </summary>
        static void UpdateCabinet()
        {
            string patternForNames = @"^[а-яА-ЯёЁa-zA-Z]+$";
            string patternForDate = @"[0-9]{4}-(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])";

            Console.WriteLine();
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
                Console.WriteLine();
                Menu();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Somthing wrong, try again");
                UpdateCabinet();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// interface for deleting cabinet
        /// </summary>
        static void DeleteCabinet()
        {
            Console.WriteLine();
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Somthing wrong, try again");
                Console.WriteLine();
                DeleteCabinet();
            }
        }

        /// <summary>
        /// interface for clear database
        /// </summary>
        static void DeleteAllCabinets()
        {
            Console.WriteLine();
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
                Console.WriteLine();
                Menu();
            }
            else if(choose.ToLower().Replace(" ", string.Empty) == "n")
            {
                Menu();
            }       
            else
            {
                Console.WriteLine();
                Console.WriteLine("Somthing wrong, try again");
                Console.WriteLine();
                DeleteAllCabinets();
            }
        }

        /// <summary>
        /// interface for expoting 
        /// </summary>
        static void ExportToCsv()
        {

            IExport<Cabinet> exportCsv = new Storage.Csv.Export.ExportCabinet("file.csv");
            exportCsv.SaveAll(cabinet.GetAll());

            Console.WriteLine("Exporting is success");
            Menu();
        }

        /// <summary>
        /// interface for expoting 
        /// </summary>
        static void ExportToXml()
        {
            Console.WriteLine();
            IExport<Cabinet> exportCsv = new Storage.Xml.Export.ExportCabinet("file.xml");
            exportCsv.SaveAll(cabinet.GetAll());

            Console.WriteLine("Exporting is success");
            Console.WriteLine();

            Menu();
        }

        /// <summary>
        /// interface for importing 
        /// </summary>
        static void ImportFromCsv()
        {
            Console.WriteLine();

            IImport<Cabinet> importCsv = new Storage.Csv.Import.ImportCabinet("file.csv");
            var cabinets = importCsv.ReadAll();
            foreach (var item in cabinets)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("Importing is success");
            Console.WriteLine();

            Menu();
        }

        /// <summary>
        /// interface for importing 
        /// </summary>
        static void ImportFromXML()
        {
            Console.WriteLine();
            IImport<Cabinet> importXML = new Storage.Xml.Import.ImportCabinet("file.xml");
            var cabinets = importXML.ReadAll();
            foreach (var item in cabinets)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("Importing is success");
            Console.WriteLine();

            Menu();
        }
    }
}
