using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class SqliteScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

        // Create database
        string connection = "URI=file:" + Application.dataPath + "/Plugins/" + "Database.db";

        // Open connection
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Read and print all values in table
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM questions";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            Debug.Log("id: " + reader[0].ToString());
            Debug.Log("text: " + reader[1].ToString());
        }

        // Close connection
        dbcon.Close();

    }

    // Update is called once per frame
    void Update()
    {

    }
}