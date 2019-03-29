using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XmlrpcAPI.Dto
{
    public class Customer
    {
        public string Name { get; set; }
        public string X_UUID { get; set; }
        public int X_timestamp { get; set; }
        public int Version { get; set; }
        public bool X_Banned { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string X_dateOfBirth { get; set; }
        public string Vat { get; set; }

        public Customer(string name, string x_UUID, int x_timestamp, int version, bool x_Banned, bool active, string email, string mobile, string x_dateOfBirth, string vat)
        {
            Name = name;
            X_UUID = x_UUID;
            X_timestamp = x_timestamp;
            Version = version;
            X_Banned = x_Banned;
            Active = active;
            Email = email;
            Mobile = mobile;
            X_dateOfBirth = x_dateOfBirth;
            Vat = vat;
        }
    }
}
