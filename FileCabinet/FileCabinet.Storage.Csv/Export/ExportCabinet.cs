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
        public ExportCabinet(string path)
        {
            this.path = path;
        }

        private string path { get; set; }

        public bool SaveAll(List<Cabinet> objects)
        {
            if (objects == null)
                throw new ArgumentNullException();
            try
            {                
                using (var writer = new StreamWriter(path))
                using (var csvWriter = new CsvWriter(writer))
                {
                    csvWriter.Configuration.Delimiter = ";";

                    csvWriter.WriteField("id");
                    csvWriter.WriteField("first name");
                    csvWriter.WriteField("last name");
                    csvWriter.WriteField("date birth");
                    csvWriter.NextRecord();

                    foreach (var obj in objects)
                    {
                        csvWriter.WriteField(obj.Id);
                        csvWriter.WriteField(obj.FirstName);
                        csvWriter.WriteField(obj.LastName);
                        csvWriter.WriteField(obj.DateBirth.ToString());
                        csvWriter.NextRecord();
                    }
                    writer.Flush();

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
