using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileCabinet.Database.Core.Models
{
    public class Cabinet
    {
        public Cabinet(int id, string firstName, string lastName, DateTime dateBirth)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateBirth = dateBirth;
        }
        public Cabinet() { }

        public int Id { get; set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public DateTime DateBirth { get; set; }

        public override string ToString()
        {
            return Id + " " + FirstName + " " + LastName + " " + DateBirth.ToString();
        }

        public override int GetHashCode()
        {
            return (Id + FirstName + LastName + DateBirth).GetHashCode();
        }
    }
}
