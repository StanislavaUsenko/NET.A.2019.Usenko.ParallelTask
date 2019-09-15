using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileCabinet.Database.Core.Interfaces; 
using FileCabinet.Database.Core.Models;
using CsvHelper;
using System.IO;

namespace FileCabinet.Storage.Csv.Import
{
    public class ImportCabinet : IImport<Cabinet>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path">path to file</param>
        public ImportCabinet(string path)
        {
            this.path = path;
        }

        private string path { get; set; }

        /// <summary>
        /// Read all records from XML file of class Cabinet 
        /// </summary>
        /// <returns>List of records</returns>
        public List<Cabinet> ReadAll()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException();
                }

                List<Cabinet> allCabinets = new List<Cabinet>();

                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords<Cabinet>().ToList();

                    return records;
                }

                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }       
        }
    }
}
