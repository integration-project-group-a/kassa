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

//https://stackoverflow.com/questions/51404705/how-to-connect-or-login-with-odoo-using-c-sharp-code-and-after-connect-with-odo
//test test test

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
        public string GetCustomers()
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

            XmlRpcStruct[] results = rpcField.Searchread(db, userId, password, "res.partner", "search_read", filter);
            List<Dto.Customer> customers = new List<Dto.Customer>();
            foreach (var res in results)
            {
                string test = JsonConvert.SerializeObject(res);
                JObject jo = JObject.Parse(test);

                Dto.Customer tempCustomer = new Dto.Customer(jo["name"].ToString(), jo["x_UUID"].ToString(), Int32.Parse(jo["x_timestamp"].ToString()),
                    Int32.Parse(jo["x_version"].ToString()), bool.Parse(jo["x_banned"].ToString()), bool.Parse(jo["active"].ToString()), 
                    jo["email"].ToString(), jo["mobile"].ToString(), jo["x_dateofbirth"].ToString(), jo["vat"].ToString());
                customers.Add(tempCustomer);
            }

            return JsonConvert.SerializeObject(customers);
            //return results;
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
            //addPairFields.Add("phone", customer.GsmNumber);
            //addPairFields.Add("x_dateofbirth", customer.DateOfBirth);
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
