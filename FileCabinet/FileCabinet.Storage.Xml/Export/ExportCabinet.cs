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
    class ExportCabinet : IExport<Cabinet>
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
