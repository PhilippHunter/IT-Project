using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
using Mono.Data.Sqlite;
using System.IO;
using Assets.Model;

public class SqliteScript
{
    static string connectionString = "URI=file:" + Application.dataPath + "/Plugins/" + "Database.db";
    //static string connectionString = "URI=file:C:/workspace/IT-Project/ITProject/Assets/Plugins/Database.db";

    private static Country getCountry(string countryString, SqliteConnection connection)
    {
        Country result;
        using (SqliteCommand command = new SqliteCommand($"SELECT * FROM countries WHERE name='{countryString}'", connection))
        {
            command.Connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result = new Country(Convert.ToInt32(reader["id"].ToString()), reader["name"].ToString());
                    Debug.Log(result.Name);
                    return result;
                }
            }
        }
        return null;
    } 

    public static List<Question> GetQuestionsByCountry(string countryString)
    {
        List<Question> result = new List<Question>();
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            Country country = getCountry(countryString, connection);
            using (SqliteCommand command = new SqliteCommand($"SELECT * FROM questions WHERE country_id={country.ID};", connection))
            {
               // command.Connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Question question = new Question(Convert.ToInt32(reader["id"]), reader["question_text"].ToString());
                        if (!result.Contains(question))
                            result.Add(question);
                    }
                }
            }
        }
        return result;
    }

    public static List<Answer> GetAnswersByQuestionId(int questionId)
    {
        List<Answer> result = new List<Answer>();
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            using (SqliteCommand command = new SqliteCommand($"SELECT * FROM answers WHERE question_id={questionId};", connection))
            {
                command.Connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Answer answer = new Answer(reader["answer_text"].ToString(), Convert.ToBoolean(reader["is_correct"]));
                        result.Add(answer);
                    }
                }
            }
        }
        return result;
    }

    public static List<Country> GetCountriesByContinent(string continentString)
    {
        List<Country> result = new List<Country>();
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            using (SqliteCommand command = new SqliteCommand(
                "SELECT country.name FROM countries AS country " +
                "JOIN continents AS continent ON country.continent_id=continent.id " +
                $"WHERE continent.name='{continentString}';"
                , connection))
            {
                command.Connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Country country = new Country(Convert.ToInt32(reader["id"].ToString()), reader["name"].ToString());
                        result.Add(country);
                    }
                }
            }
        }
        return result;
    }

    public static void SetQuizCompleted(string countryString)
    {
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(
                "UPDATE countries SET is_completed=1 " +
                $"WHERE name='{countryString}';", connection))
            {
                command.ExecuteReader();
            }
        }
    }
}