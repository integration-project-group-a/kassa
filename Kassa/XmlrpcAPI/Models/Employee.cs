using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XmlrpcAPI.Models
{
    public class Employee
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Timestamp { get; set; }
        public int Version { get; set; }
        public bool Active { get; set; }

        public Employee(string uuid, string name, string email, int timestamp, int version, bool active)
        {
            this.UUID = uuid;
            this.Name = name;
            this.Email = email;
            this.Timestamp = timestamp;
            this.Version = version;
            this.Active = active;
        }
    }
}
