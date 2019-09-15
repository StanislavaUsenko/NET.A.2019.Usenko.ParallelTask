using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FileCabinet.Database.Core.Interfaces;
using FileCabinet.Database.Core.Models;

namespace FileCabinet.Storage.Xml.Import
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
                XmlSerializer serialiser = new XmlSerializer(typeof(List<Cabinet>));
                using (TextReader FileStream = new StreamReader(path))
                {
                    IEnumerable<Cabinet> cabinets = (IEnumerable<Cabinet>) serialiser.Deserialize(FileStream);
                    List<Cabinet> listForReturn = new List<Cabinet>();
                    foreach (var cab in cabinets)
                    {
                        listForReturn.Add(cab);
                    }
                    return listForReturn;
                }

                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
