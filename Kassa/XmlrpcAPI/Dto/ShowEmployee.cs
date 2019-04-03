using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XmlrpcAPI.Dto
{
    public class ShowEmployee
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Partner_id { get; set; }
        public string X_UUID { get; set; }
        public int X_Timestamp { get; set; }
        public int X_Version { get; set; }
        public bool X_Banned { get; set; }
        public bool Active { get; set; }

        public ShowEmployee(string name, string email, int partner_id, string x_UUID, int x_Timestamp, int x_Version, bool x_Banned, bool active)
        {
            Name = name;
            Email = email;
            Partner_id = partner_id;
            X_UUID = x_UUID;
            X_Timestamp = x_Timestamp;
            X_Version = x_Version;
            X_Banned = x_Banned;
            Active = active;
        }
    }
}
