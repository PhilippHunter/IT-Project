using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using Assets.Model;

public static class SqliteScript 
{
    static string connectionString = $"URI=file:{Application.dataPath}/Plugins/Database.db";
    // Use this for initialization
    //void Start()
    //{
    //    // Create database
    //    string connection = "URI=file:" + Application.dataPath + "/Plugins/" + "Database.db";

    //    // Open connection
    //    IDbConnection dbcon = new SqliteConnection(connection);
    //    dbcon.Open();

    //    // Read and print all values in table
    //    IDbCommand cmnd_read = dbcon.CreateCommand();
    //    IDataReader reader;
    //    string query = "SELECT * FROM questions";
    //    cmnd_read.CommandText = query;
    //    reader = cmnd_read.ExecuteReader();

    //    while (reader.Read())
    //    {
    //        Debug.Log("id: " + reader[0].ToString());
    //        Debug.Log("text: " + reader[1].ToString());
    //    }

    //    // Close connection
    //    dbcon.Close();

    //}

    private static Country getCountry(string countryString, SqliteConnection connection)
    {
        Country result;
        using (SqliteCommand command = new SqliteCommand($"SELECT * FROM countries WHERE name={countryString}, {connection}"))
        {
            command.Connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result = new Country(reader["country_text"].ToString());
                    Debug.Log(result);
                    return result;
                }
            }
        }
        return null;
    } 

    public static List<Question> GetQuestionByCountry(string countryString)
    {
        List<Question> result = new List<Question>();
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            Country country = getCountry(countryString, connection);
            using (SqliteCommand command = new SqliteCommand("SELECT * FROM questions;", connection))
            {
                command.Connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Question question = new Question(reader["question_text"].ToString());
                        if (!result.Contains(question))
                            result.Add(question);
                    }
                }
            }
        }
        return result;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}