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
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="dateBirth"></param>
        public Cabinet(int id, string firstName, string lastName, DateTime dateBirth)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateBirth = dateBirth;
        }
        /// <summary>
        /// standart ctor
        /// </summary>
        public Cabinet() { }

        /// <summary>
        /// id of record
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// First name field
        /// </summary>
        public string FirstName { get;  set; }
        /// <summary>
        /// last name field
        /// </summary>
        public string LastName { get;  set; }
        /// <summary>
        /// date of birth field
        /// </summary>
        public DateTime DateBirth { get; set; }

        /// <summary>
        /// Specialized method to convert objetc to string
        /// </summary>
        /// <returns>String of object</returns>
        public override string ToString()
        {
            return Id + " " + FirstName + " " + LastName + " " + DateBirth.Day + "." + DateBirth.Month + "." + DateBirth.Year;
        }

        /// <summary>
        /// Specialized method for getting hash-code
        /// </summary>
        /// <returns> int hash-code</returns>
        public override int GetHashCode()
        {
            return (Id + FirstName + LastName + DateBirth).GetHashCode();
        }
    }
}
