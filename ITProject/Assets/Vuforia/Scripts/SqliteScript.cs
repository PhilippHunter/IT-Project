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
    static string connectionString = "URI=file:" + Application.persistentDataPath + "/" + "Database.db";

    #region CreateDbScripts
    const string CREATE_TABLES =
                "CREATE TABLE IF NOT EXISTS answers(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, answer_text TEXT, is_correct INTEGER, question_id INTEGER NOT NULL, FOREIGN KEY(question_id) REFERENCES questions(id));" +
                "CREATE TABLE IF NOT EXISTS continents(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name TEXT, is_unlocked INTEGER DEFAULT 0);" +
                "CREATE TABLE IF NOT EXISTS countries(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, continent_id INTEGER, is_completed INTEGER DEFAULT 0);" +
                "CREATE TABLE IF NOT EXISTS information(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, information_text TEXT NOT NULL, country_id INTEGER NOT NULL);" +
                "CREATE TABLE IF NOT EXISTS questions(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, question_text TEXT, country_id INTEGER);";

    const string INSERT_CONTINTENTS =
                "INSERT INTO continents(id, name, is_unlocked) VALUES('1', 'Africa', '0');" +
                "INSERT INTO continents(id, name, is_unlocked) VALUES('2', 'Antarctica', '0');" +
                "INSERT INTO continents(id, name, is_unlocked) VALUES('3', 'Australia', '0');" +
                "INSERT INTO continents(id, name, is_unlocked) VALUES('4', 'Asia', '0');" +
                "INSERT INTO continents(id, name, is_unlocked) VALUES('5', 'Europe', '0');" +
                "INSERT INTO continents(id, name, is_unlocked) VALUES('6', 'North-America', '0');" +
                "INSERT INTO continents(id, name, is_unlocked) VALUES('7', 'South-America', '0');";

    const string INSERT_COUNTRIES =
                "INSERT INTO countries(id, name, continent_id, is_completed) VALUES('1', 'Germany', '5', '0');" +
                "INSERT INTO countries(id, name, continent_id, is_completed) VALUES('2', 'France', '5', '0');" +
                "INSERT INTO countries(id, name, continent_id, is_completed) VALUES('3', 'China', '4', '0');" +
                "INSERT INTO countries(id, name, continent_id, is_completed) VALUES('4', 'India', '4', '0');" +
                "INSERT INTO countries(id, name, continent_id, is_completed) VALUES('5', 'Thailand', '4', '0');";

    const string INSERT_QUESTIONS =
                "INSERT INTO questions(id, question_text, country_id) VALUES('1', 'Welche Währung wird hier genutzt?', '1');" +
                "INSERT INTO questions(id, question_text, country_id) VALUES('2', 'Oui?', '2');" +
                "INSERT INTO questions(id, question_text, country_id) VALUES('3', 'Wann war der Mauerfall?', '1');" +
                "INSERT INTO questions(id, question_text, country_id) VALUES('4', 'Wie viele Einwohner hat Deutschland?', '1');" +
                "INSERT INTO questions(id, question_text, country_id) VALUES('5', 'Welche Farbe ist nicht auf der Flagge zu finden?', '1');";

    const string INSERT_ANSWERS =
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('1', 'Euro', '1', '1');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('2', 'D-Mark', '0', '1');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('3', 'Dollar', '0', '1');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('4', 'Pfund', '0', '1');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('5', '1989', '1', '3');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('6', '1990', '0', '3');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('7', '1975', '0', '3');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('8', '1970', '0', '3');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('9', 'Braun', '1', '5');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('10', 'Schwarz', '0', '5');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('11', 'Gold', '0', '5');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('12', 'Rot', '0', '5');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('13', 'Si', '1', '2');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('14', 'Yes', '0', '2');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('15', 'Wie', '0', '2');" +
                "INSERT INTO answers(id, answer_text, is_correct, question_id) VALUES('16', 'Ja', '0', '2');";

    const string INSERT_INFORMATION =
                "INSERT INTO information(id, information_text, country_id) VALUES('1', 'Die Hauptstadt von Deutschland ist Berlin.', '1');" +
                "INSERT INTO information(id, information_text, country_id) VALUES('2', 'Die Deutschlandflagge ist schwarz, rot und gold.', '1');" +
                "INSERT INTO information(id, information_text, country_id) VALUES('3', 'Die Hauptstadt von Frankreich ist Paris.', '2');" +
                "INSERT INTO information(id, information_text, country_id) VALUES('4', 'Die französische Flagge ist blau, rot und weiß.', '2');" +
                "INSERT INTO information(id, information_text, country_id) VALUES('5', 'Die Hauptstadt von China ist Peking.', '3');" +
                "INSERT INTO information(id, information_text, country_id) VALUES('6', 'Übersetzt bedeutet das Schriftzeichen für China: Land der Mitte.', '3');" +
                "INSERT INTO information(id, information_text, country_id) VALUES('7', 'Die Hauptstadt von Indien ist Delhi.', '4');" +
                "INSERT INTO information(id, information_text, country_id) VALUES('8', 'Inder essen gerne scharf.', '4');" +
                "INSERT INTO information(id, information_text, country_id) VALUES('9', 'Die Hauptstadt von Thailand ist Bangkok.', '5');" +
                "INSERT INTO information(id, information_text, country_id) VALUES('10', 'Die Flagge von Thailand ist rot, blau und weiß.', '5');";

    #endregion

    public static void CreateDB()
    {
        if (!File.Exists(Application.persistentDataPath + "/Database.db"))
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {

                connection.Open();

                SqliteCommand cmd = connection.CreateCommand();
                string queryString = "";

                queryString = CREATE_TABLES;
                cmd.CommandText = queryString;
                cmd.ExecuteNonQuery();

                queryString = INSERT_CONTINTENTS;
                cmd.CommandText = queryString;
                cmd.ExecuteNonQuery();

                queryString = INSERT_COUNTRIES;
                cmd.CommandText = queryString;
                cmd.ExecuteNonQuery();

                queryString = INSERT_QUESTIONS;
                cmd.CommandText = queryString;
                cmd.ExecuteNonQuery();

                queryString = INSERT_ANSWERS;
                cmd.CommandText = queryString;
                cmd.ExecuteNonQuery();

                queryString = INSERT_INFORMATION;
                cmd.CommandText = queryString;
                cmd.ExecuteNonQuery();
            }
        }
    }

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

    public static List<Information> GetInformationByCountry(string countryString)
    {
        List<Information> result = new List<Information>();
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            Country country = getCountry(countryString, connection);
            using (SqliteCommand command = new SqliteCommand($"SELECT * FROM information WHERE country_id={country.ID};", connection))
            {
                // command.Connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Information question = new Information(Convert.ToInt32(reader["id"]), reader["information_text"].ToString());
                        if (!result.Contains(question))
                            result.Add(question);
                    }
                }
            }
        }
        return result;
    }

    //this is now working
    public static List<Country> GetCountriesByContinent(string continentString)
    {
        List<Country> result = new List<Country>();
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            using (SqliteCommand command = new SqliteCommand($"SELECT * FROM countries AS country " +
                "JOIN continents AS continent ON country.continent_id=continent.id " +
                $"WHERE continent.name='{continentString}';", connection))
            {
                command.Connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Country country = new Country(Convert.ToInt32(reader["id"]), reader["name"].ToString());
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