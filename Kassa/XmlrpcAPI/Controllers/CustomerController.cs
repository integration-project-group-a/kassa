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

//https://stackoverflow.com/questions/51404705/how-to-connect-or-login-with-odoo-using-c-sharp-code-and-after-connect-with-odo

namespace XmlrpcAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly string db = "testDB";
        readonly string username = "ilja.de.rycke@student.ehb.be";
        readonly string password = "test123";

        // GET: api/Customer
        [HttpGet]
        public object[] GetCustomers()
        {
            IOpenErpLogin rpcClientLogin = XmlRpcProxyGen.Create<IOpenErpLogin>();
            rpcClientLogin.NonStandard = XmlRpcNonStandard.AllowStringFaultCode;

            //login
            int userId = rpcClientLogin.Login(db, username, password);

            //if(userId > 0)
            //{
            //    return "login ok";
            //} else
            //{
            //    return "login failed";
            //}

            IOpenErpAddFields rpcField = XmlRpcProxyGen.Create<IOpenErpAddFields>();

            //get all customers
            object[] filter = new object[1];
            filter[0] = new object[3] { "customer", "=", "true" };

            //List fields = new object[1];
            //object[] fieldsParams = new object[2] { "name", "street" };
            //fields = new object[2] { "fields", fieldsParams };

            object[] results = rpcField.Searchread(db, userId, password, "res.partner", "search_read", filter);

            return results;
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public string GetCustomer(int id)
        {
            return "value";
        }

        // POST: api/Customer
        [HttpPost]
        public string PostCustomer([FromBody] Customer customer)
        {
            IOpenErpLogin rpcClientLogin = XmlRpcProxyGen.Create<IOpenErpLogin>();
            rpcClientLogin.NonStandard = XmlRpcNonStandard.AllowStringFaultCode;

            //login
            int userId = rpcClientLogin.Login(db, username, password);

            //Add Contacts(Customers)
            IOpenErpAddFields rpcField = XmlRpcProxyGen.Create<IOpenErpAddFields>();
            XmlRpcStruct addPairFields = new XmlRpcStruct();
            addPairFields.Add("x_UUID", customer.UUID);
            addPairFields.Add("name", customer.Name);
            addPairFields.Add("email", customer.Email);
            addPairFields.Add("x_timestamp", customer.Timestamp);
            addPairFields.Add("x_version", customer.Version);
            addPairFields.Add("active", customer.Active);
            addPairFields.Add("x_banned", customer.Banned);
            addPairFields.Add("customer", true);

            int resAdd = rpcField.Create("testDB", userId, "test123", "res.partner", "create", addPairFields);

            return "Customer created with id = " + resAdd;
        }

        // PUT: api/Customer/5
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
