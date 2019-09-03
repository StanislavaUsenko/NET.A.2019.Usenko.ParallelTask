﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinet.Database.Core.Models
{
    class Cabinet
    {
        public Cabinet(int id, string firstName, string lastName, DateTime dateBirth)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateBirth = dateBirth;
        }

        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateBirth { get; private set; }

    }
}