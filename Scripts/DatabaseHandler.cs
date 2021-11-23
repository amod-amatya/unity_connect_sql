using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using MySql.Data;
using MySql.Data.MySqlClient;

public class DatabaseHandler : MonoBehaviour
{
    [Serializable]
    private string host, database, user, password, port;
    public bool pooling = true;
    private string connectionString;
    private MySqlConnection con = null;
    private MySqlCommand cmd = null;
    private MySqlDataReader rdr = null;
    public GameObject textTemplate;  

    void Awake() {
        DontDestroyOnLoad(this.gameObject);

        connectionString = "Server=" + host + 
                            ";Database=" + database + 
                            ";UserID=" + user + 
                            ";Password=" + password + 
                            ";Port=" + port + 
                            ";SslMode=Required;CertificateStoreLocation=None;Pooling=";
        Debug.Log("Connection string is " + connectionString);
        if (pooling)
        {
            connectionString += "True";
        }
        else
        {
            connectionString += "False";
        }
        try
        {
            int i;
            con = new MySqlConnection(connectionString);
            con.Open();
            Debug.Log("Mysql Connection state: " + con.State);

            string sql = "SELECT * FROM Component_View";
            cmd = new MySqlCommand(sql, con);
                        cmd = new MySqlCommand(sql, con);
                        rdr = cmd.ExecuteReader();
            
                        while (rdr.Read())
                        {
                            i = 0;
                            Debug.Log("Reading Rows of Component_View");
                            populateList(rdr[1].ToString());
                            Debug.Log(rdr[0]+" -- "+rdr[1]);
                            i++;
                        }
                       rdr.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Exception");
            Debug.Log(e);
        }
    }
    public void populateList(string data)
    {
        GameObject txt;
        txt = Instantiate(textTemplate) as GameObject;
        txt.SetActive(true);

        txt.GetComponent<DBControlList>().SetText(data);
        txt.transform.SetParent(textTemplate.transform.parent, false);
    }
}
