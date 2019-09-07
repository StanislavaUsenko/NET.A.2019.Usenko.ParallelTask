using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileCabinet.Database.Core.Models;
using FileCabinet.Database.Core.Interfaces;
using System.Xml.Serialization;
using System.IO;

namespace FileCabinet.Storage.Xml.Export
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
        /// save all records from database table Cabinet to XML file
        /// </summary>
        /// <param name="objects">records what need to save</param>
        /// <returns>flag of success operation</returns>
        public bool SaveAll(List<Cabinet> objects)
        {
            if (objects == null)
                throw new ArgumentNullException();
            try
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(List<Cabinet>));
                using (TextWriter FileStream = new StreamWriter(path))
                {
                    serialiser.Serialize(FileStream, objects);
                }

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
