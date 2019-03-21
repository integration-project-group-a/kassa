package API;


import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;

import java.net.MalformedURLException;
import java.net.URL;
import static java.util.Arrays.asList;

public class Odoo_api {

    public static void main(String[] args) throws MalformedURLException, XmlRpcException {
        String url = "http://localhost:8069"; // work with odoo.com account!!
        String db = "Kassa";
        String username = "ward.pille@student.ehb.be";
        String password = "admin";
        System.out.println("Get database list");
        System.out.println("Login");
        System.out.println("--------------");
        int uid = login(url,db,username,password);
        if (uid >0) {
            System.out.println("Login Ok");
        } else {
            System.out.println("Login Fail");
        }

        seartch(url,db,uid,password);



    }
    // login
    static int login(String url, String db, String login, String password) throws XmlRpcException, MalformedURLException {
        XmlRpcClient client = new XmlRpcClient();
        XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();
        config.setEnabledForExtensions(true);
        //config.setServerURL(new URL(url+"/xmlrpc/common"));
        config.setServerURL(new URL(url + "/xmlrpc/2/common"));
        client.setConfig(config);
        //Connect
        //Object[] empty = null; // Ok
        //Object[] params = new Object[] {db,login,password, empty}; // Ok
        Object[] params = new Object[]{db, login, password}; // Ok & simple
        Object uid = client.execute("login", params);
        if (uid instanceof Integer)
            return (int) uid;
        return -1;
    }

    static void seartch(String url, String db, int uid, String password) throws XmlRpcException, MalformedURLException {
        final XmlRpcClient models = new XmlRpcClient() {{
            setConfig(new XmlRpcClientConfigImpl() {{
                setServerURL(new URL(String.format("%s/xmlrpc/2/object", url)));
            }});
        }};

        System.out.println(asList((Object[])models.execute("execute_kw", asList(
                db, uid, password,
                "res.partner", "search_read"
        ))));
    }
}
