using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DateTime DateOfBirth { get; set; }
        public string BtwNumber { get; set; }
        public string GsmNumber {get; set;}
        

        public Customer(string uuid, string name, string email, int timestamp, int version, bool active, bool banned, DateTime dateOfBirth, string btwNumber, string gsmNumber)
        {
            this.UUID = uuid;
            this.Name = name;
            this.Email = email;
            this.Timestamp = timestamp;
            this.Version = version;
            this.Active = active;
            this.Banned = banned;
            this.DateOfBirth = dateOfBirth;
            this.BtwNumber = btwNumber;
            this.GsmNumber = gsmNumber;
            
        }
    }
}
