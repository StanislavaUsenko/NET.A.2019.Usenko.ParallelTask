using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using FileCabinet.Database.Core.Models;
using FileCabinet.Database.Core.Interfaces;



namespace FileCabinet.Storage.Csv.Export
{
    public class ExportCabinet : IExport<Cabinet>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path">path to file</param>
        public ExportCabinet(string path)
        {
            this.path = path;
        }

        private string path { get; set; }

        /// <summary>
        /// save all records from database table Cabinet to Csv file
        /// </summary>
        /// <param name="objects">records what need to save</param>
        /// <returns>flag of success operation</returns>
        public bool SaveAll(List<Cabinet> objects)
        {
            if (objects == null)
                throw new ArgumentNullException();
            try
            {                
                using (var writer = new StreamWriter(path))
                using (var csvWriter = new CsvWriter(writer))
                {
                    {
                        csvWriter.WriteRecords(objects);
                    }

                    return true;
                }
            }
            catch(Exception ex)
            {
                throw new Exception();
            }            
        }
    }
}
