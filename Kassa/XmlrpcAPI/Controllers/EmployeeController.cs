using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horizon.XmlRpc.Client;
using Horizon.XmlRpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XmlrpcAPI.Interfaces;
using XmlrpcAPI.Models;

namespace XmlrpcAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        readonly string db = "testDB";
        readonly string username = "ilja.de.rycke@student.ehb.be";
        readonly string password = "test123";

        // GET: api/Employee
        [HttpGet]
        public string GetEmployees()
        {
            TimeSpan start = DateTime.Now.TimeOfDay;
            IOpenErpLogin rpcClientLogin = XmlRpcProxyGen.Create<IOpenErpLogin>();
            rpcClientLogin.NonStandard = XmlRpcNonStandard.AllowStringFaultCode;

            //login
            int userId = rpcClientLogin.Login(db, username, password);

            IOpenErpAddFields rpcField = XmlRpcProxyGen.Create<IOpenErpAddFields>();

            //get all employees by name
            object[] filter = new object[1];
            filter[0] = new object[3] { "employee", "=", "true" };

            //List fields = new object[1];
            //object[] fieldsParams = new object[2] { "name", "street" };
            //fields = new object[2] { "fields", fieldsParams };

            XmlRpcStruct[] results = rpcField.Searchread(db, userId, password, "res.users", "search_read", filter);
            TimeSpan end = DateTime.Now.TimeOfDay;
            TimeSpan delta = end - start;
            List<Dto.ShowEmployee> employees = new List<Dto.ShowEmployee>();
            foreach (var res in results)
            {
                string test = JsonConvert.SerializeObject(res);
                JObject jo = JObject.Parse(test);

                Dto.ShowEmployee tempEmployee = new Dto.ShowEmployee(jo["name"].ToString(), jo["email"].ToString(), Int32.Parse(jo["partner_id"][0].ToString()), jo["x_UUID"].ToString(),
                    Int32.Parse(jo["x_timestamp"].ToString()), Int32.Parse(jo["x_version"].ToString()), bool.Parse(jo["x_banned"].ToString()), bool.Parse(jo["active"].ToString()));
                employees.Add(tempEmployee);
            }

            
            return JsonConvert.SerializeObject(employees) + "\n" + delta.ToString();
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "GetEmployee")]
        public string GetEmployee(int id)
        {
            return "value";
        }

        // POST: api/Employee
        [HttpPost]
        public string PostEmployee([FromBody] Employee employee)
        {
            IOpenErpLogin rpcClientLogin = XmlRpcProxyGen.Create<IOpenErpLogin>();
            rpcClientLogin.NonStandard = XmlRpcNonStandard.AllowStringFaultCode;

            //login
            int userId = rpcClientLogin.Login(db, username, password);

            //Add Contacts(Customers)
            IOpenErpAddFields rpcField = XmlRpcProxyGen.Create<IOpenErpAddFields>();
            XmlRpcStruct addPairFields = new XmlRpcStruct();

            addPairFields.Add("x_UUID", employee.UUID);
            addPairFields.Add("name", employee.Name);
            addPairFields.Add("email", employee.Email);
            addPairFields.Add("x_timestamp", employee.Timestamp);
            addPairFields.Add("x_version", employee.Version);
            addPairFields.Add("active", employee.Active);
            addPairFields.Add("employee", true);
            addPairFields.Add("login", employee.Email);
            addPairFields.Add("sel_groups_1_9_10", 1);
            addPairFields.Add("sel_groups_38_39", 38);

            int resAdd = rpcField.Create(db, userId, password, "res.users", "create", addPairFields);

            return "Employee created with id = " + resAdd;
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
