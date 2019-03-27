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
    public class CustomerController : ControllerBase
    {
        // GET: api/Customer
        [HttpGet]
        public object[] Get()
        {
            IOpenErpLogin rpcClientLogin = XmlRpcProxyGen.Create<IOpenErpLogin>();
            rpcClientLogin.NonStandard = XmlRpcNonStandard.AllowStringFaultCode;

            //login
            int userId = rpcClientLogin.Login("testDB", "ilja.de.rycke@student.ehb.be", "test123");

            //if(userId > 0)
            //{
            //    return "login ok";
            //} else
            //{
            //    return "login failed";
            //}

            IOpenErpAddFields rpcField = XmlRpcProxyGen.Create<IOpenErpAddFields>();

            //get all customers by name
            object[] filter = new object[1];
            filter[0] = new object[3] { "customer", "=", "true" };

            //List fields = new object[1];
            //object[] fieldsParams = new object[2] { "name", "street" };
            //fields = new object[2] { "fields", fieldsParams };

            object[] results = rpcField.Searchread("testDB", userId, "test123", "res.partner", "search_read", filter);

            return results;
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            IOpenErpLogin rpcClientLogin = XmlRpcProxyGen.Create<IOpenErpLogin>();
            rpcClientLogin.NonStandard = XmlRpcNonStandard.AllowStringFaultCode;

            //login
            int userId = rpcClientLogin.Login("testDB", "ilja.de.rycke@student.ehb.be", "test123");

            //Add Contacts(Customers)
            IOpenErpAddFields rpcField = XmlRpcProxyGen.Create<IOpenErpAddFields>();
            XmlRpcStruct addPairFields = new XmlRpcStruct();
            addPairFields.Add("name", customer.Name);
            addPairFields.Add("street", customer.Street);
            addPairFields.Add("city", customer.City);
            addPairFields.Add("zip", customer.Zip);
            addPairFields.Add("email", customer.Email);
            int resAdd = rpcField.Create("testDB", userId, "test123", "res.partner", "create", addPairFields);
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
