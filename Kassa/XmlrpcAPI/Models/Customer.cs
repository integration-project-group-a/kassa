using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XmlrpcAPI.Models
{
    public class Customer
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Timestamp { get; set; }
        public int Version { get; set; }
        public bool Active { get; set; }
        public bool Banned { get; set; }
        public string GsmNumber {get; set;}
        public DateTime DateOfBirth { get; set; }


        public Customer(string uuid, string name, string email, int timestamp, int version, bool active, bool banned, string gsmNumber, DateTime dateOfBirth)
        {
            this.UUID = uuid;
            this.Name = name;
            this.Email = email;
            this.Timestamp = timestamp;
            this.Version = version;
            this.Active = active;
            this.Banned = banned;
            this.GsmNumber = gsmNumber;
            this.DateOfBirth = dateOfBirth;
        }
    }
}
