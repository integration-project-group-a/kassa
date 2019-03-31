using Horizon.XmlRpc.Client;
using Horizon.XmlRpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XmlrpcAPI.Interfaces
{
    [XmlRpcUrl(" http://10.3.56.23:8069/xmlrpc/2/object")]
    public interface IOpenErpAddFields : IXmlRpcProxy
    {
        [XmlRpcMethod("execute")]
        int Create(string dbName, int userId, string dbPwd, string model, string method, XmlRpcStruct fieldValues);
        [XmlRpcMethod("execute")]
        object[] Read(string dbName, int userId, string dbPwd, string model, string method, int[] ids, object[] fields);
        [XmlRpcMethod("execute")]
        int[] Search(string dbName, int userId, string dbPwd, string model, string method, object[] filter);
        [XmlRpcMethod("execute")]
        int Countsearch(string dbName, int userId, string dbPwd, string model, string method, object[] filter);  //works!!!
        [XmlRpcMethod("execute")]
        XmlRpcStruct[] Searchread(string dbName, int userId, string dbPwd, string model, string method, object[] filter);
    }
}
