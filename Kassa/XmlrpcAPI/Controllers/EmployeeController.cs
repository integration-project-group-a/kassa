using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horizon.XmlRpc.Client;
using Horizon.XmlRpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public object[] GetEmployees()
        {
            IOpenErpLogin rpcClientLogin = XmlRpcProxyGen.Create<IOpenErpLogin>();
            rpcClientLogin.NonStandard = XmlRpcNonStandard.AllowStringFaultCode;

            //login
            int userId = rpcClientLogin.Login(db, username, password);

            IOpenErpAddFields rpcField = XmlRpcProxyGen.Create<IOpenErpAddFields>();

            //get all employees by name
            object[] filter = new object[1];
            filter[0] = new object[3] { "name", "=", "yasmine user" };

            //List fields = new object[1];
            //object[] fieldsParams = new object[2] { "name", "street" };
            //fields = new object[2] { "fields", fieldsParams };

            object[] results = rpcField.Searchread(db, userId, password, "res.users", "search_read", filter);

            return results;
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
