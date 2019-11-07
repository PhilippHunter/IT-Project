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
                "CREATE TABLE IF NOT EXISTS countries(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, continent_id INTEGER, is_completed INTEGER DEFAULT 0, FOREIGN KEY(continent_id) REFERENCES continents(id));" +
                "CREATE TABLE IF NOT EXISTS information(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, information_text TEXT NOT NULL, country_id INTEGER NOT NULL, FOREIGN KEY(country_id) REFERENCES countries(id));" +
                "CREATE TABLE IF NOT EXISTS questions(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, question_text TEXT, country_id INTEGER, FOREIGN KEY(country_id) REFERENCES countries(id));";

    const string INSERT_CONTINTENTS =
                "INSERT INTO continents(name, is_unlocked) VALUES('Afrika', '0');" +        //1
                "INSERT INTO continents(name, is_unlocked) VALUES('Australien', '0');" +    //2
                "INSERT INTO continents(name, is_unlocked) VALUES('Asien', '0');" +         //3
                "INSERT INTO continents(name, is_unlocked) VALUES('Europa', '0');" +        //4
                "INSERT INTO continents(name, is_unlocked) VALUES('Nordamerika', '0');" +   //5
                "INSERT INTO continents(name, is_unlocked) VALUES('Südamerika', '0');";     //6
    #region Countries
    const string INSERT_COUNTRIES =

                //Afrika
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Aegypten', '1', '0');" +   //1
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Kongo', '1', '0');" +      //2
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Suedafrika', '1', '0');" + //3

                //Asien
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Indien', '3', '0');" +     //4
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Japan', '3', '0');" +      //5
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Malaysia', '3', '0');" +   //6

                //Europa
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Deutschland', '4', '0');" +//7
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Frankreich', '4', '0');" + //8
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Niederlande', '4', '0');" +//9

                //Nordamerika
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('USA', '5', '0');" +        //10
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Kanada', '5', '0');" +     //11
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Mexiko', '5', '0');" +     //12

                //Suedamerika
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Argentinien', '6', '0');" +//13
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Brasilien', '6', '0');" +  //14
                "INSERT INTO countries(name, continent_id, is_completed) VALUES('Peru', '6', '0');";        //15

    #endregion

    #region Questions
    const string INSERT_QUESTIONS =
                //Ägypten
                "INSERT INTO questions(question_text, country_id) VALUES('Wie viele Einwohner hat Kairo?', '1');" +
                "INSERT INTO questions(question_text, country_id) VALUES('In Ägypten spricht man…', '1');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Ägypten liegt in der Rangliste der sonnigsten Länder auf Platz:', '1');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Welches Weltkulturerbe liegt in Ägypten?', '1');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Welches Tier war im alten Ägypten heilig?', '1');" +
                //Kongo
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist die offizielle Sprache im Kongo?', '2');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Auf welchem Platz ist der Kongo von den größten Ländern in Afrika? ', '2');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Welche Landschaftsform findet man nicht im Kongo? ', '2');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Nach was wurde die Demokratische Republik Kongo benannt?', '2');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Welcher Staat kolonialisierte den Kongo bis 1960?', '2');" +
                //Südafrika
                "INSERT INTO questions(question_text, country_id) VALUES('Welche dieser 4 Städte ist keine Hauptstadt Südafrikas?', '3');" +
                "INSERT INTO questions(question_text, country_id) VALUES('In Südafrika lebt eine große Vielfalt von…', '3');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wie hoch sind die Tugela Wasserfälle in Südafrika ungefähr?', '3');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wie viele verschiedene Sprachen werden in Südafrika gesprochen?', '3');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wann wurde die Rassentrennung in Südafrika abgeschafft?', '3');" +

                //Indien
                "INSERT INTO questions(question_text, country_id) VALUES('Welche Sprache wird in Indien am häufigsten gesprochen?', '4');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Indien ist in der Rangliste der meistbevölkertsten Länder auf Platz…', '4');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was trinken Inder am liebsten?', '4');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was wurde in Indien erfunden?', '4');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was befindet sich in Indiens Hauptstadt?', '4');" +
                //Japan
                "INSERT INTO questions(question_text, country_id) VALUES('Der älteste Mensch auf der Erde ist Japaner.Wie alt ist er?', '5');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Aus wie vielen Inseln besteht Japan?', '5');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Welche japanische Insel ist die größte?', '5');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist die Hauptstadt von Japan?', '5');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wie viele Millionen Einwohner leben in der Metropolregion Tokio?', '5');" +
                //Malaysia
                "INSERT INTO questions(question_text, country_id) VALUES('Welches dieser Völker lebt nicht in Malaysia?', '6');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wie viele Stockwerke haben die Petrona Towers?', '6');" +
                "INSERT INTO questions(question_text, country_id) VALUES('In Malaysia lebt die Weltgrößte Population von…', '6');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Welche Religion findet man in Malaysia am Häufigsten?', '6');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist das Nationaltier von Malaysia?', '6');" +

                //Deutschland
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist die beliebteste Sehenswürdigkeit in Deutschland?', '7');" +
                "INSERT INTO questions(question_text, country_id) VALUES('In Berlin gibt es den größten…', '7');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wie viele verschiedene Sorten Brot werden in Deutschland gebacken?', '7');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist die Hauptstadt von Deutschland?', '7');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wann wurde Deutschland zu einem Land vereinigt?', '7');" +
                //Frankreich
                "INSERT INTO questions(question_text, country_id) VALUES('Auf der Rangliste der größten Länder Europas liegt Frankreich auf Platz…', '8');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Welcher Wert taucht nicht im Nationalmotto Frankreichs auf?', '8');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wie heißt der höchste Berg Europas, der in Frankreich ist?', '8');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist eine besondere Spezialität Frankreichs?', '8');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist die Hauptstadt von Frankreich?', '8');" +
                //Niederlande
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist das beliebteste Fortbewegungsmittel in der Niederlande?', '9');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wie viel Prozent der Niederlande liegen unter dem Meeresspiegel?', '9');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was essen Niederländer besonders gerne?', '9');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wo befindet sich der Regierungssitz der Niederlande?', '9');" +
                "INSERT INTO questions(question_text, country_id) VALUES('In den Niederlanden gibt’s es den größten…', '9');" +

                //USA
                "INSERT INTO questions(question_text, country_id) VALUES('Wie viele Staaten gibt es in den USA?', '10');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist die Hauptstadt von den USA?', '10');" +
                "INSERT INTO questions(question_text, country_id) VALUES('An wie viele Länder grenzt die USA?', '10');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was wird als Hollywood bezeichnet?', '10');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wie heißen die Ureinwohner der USA?', '10');" +
                //Kanada
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist die Hauptstadt von Kanada?', '11');" +
                "INSERT INTO questions(question_text, country_id) VALUES('In welchem Jahr wurde Kanada zu einem unabhängigen Land?', '11');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Kanada ist das … Land der Welt.', '11');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Welche Amtssprachen gibt es in Kanada?', '11');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wie viele Seen gibt es in Kanada?', '11');" +
                //Mexiko
                "INSERT INTO questions(question_text, country_id) VALUES('Sehr bekannte Sehenswürdigkeiten in Mexiko sind…', '12');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wie heißt die Hauptstadt von Mexiko?', '12');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Welche Sprache sprechen Mexikaner?', '12');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist in Mexiko ein Sombrero?', '12');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was tun die Einwohner Mexikos am Tag der Toten?', '12');" +

                //Argentinien
                "INSERT INTO questions(question_text, country_id) VALUES('Wie heißt der höchste Berg Argentiniens?', '13');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Welche Völker sind in Argentinien sehr häufig zu finden?', '13');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Etwa jeder dritte Argentinier lebt…', '13');" +
                "INSERT INTO questions(question_text, country_id) VALUES('In Argentinien gibt es nahezu…', '13');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wie heißt die Währung in Argentinien?', '13');" +
                //Brasilien
                "INSERT INTO questions(question_text, country_id) VALUES('Welche Sprache spricht man in Brasilien?', '14');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist die Hauptstadt von Brasilien?', '14');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Welche Sportart ist in Brasilien sehr am beliebtesten?', '14');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Brasilien besitzt den … Fluss der Erde.', '14');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Was ist das bekannteste Fest in Brasilien?', '14');" +
                //Peru
                "INSERT INTO questions(question_text, country_id) VALUES('Machu Picchu ist eine…', '15');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wie hoch sind die höchsten Wasserfälle Peru’s, die Gocta Fälle?', '15');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Welche Landschaftszone gibt es in Peru nicht?', '15');" +
                "INSERT INTO questions(question_text, country_id) VALUES('In Peru gibt es über 1800 verschiedene…', '15');" +
                "INSERT INTO questions(question_text, country_id) VALUES('Wo liegt die Hauptstadt Lima?', '15');";
    #endregion

    #region Answers
    const string INSERT_ANSWERS =
                //Ägypten
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('10mio', '0', '1');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('14mio', '0', '1');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('18mio', '0', '1');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('22mio', '1', '1');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('arabisch', '1', '2');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('jüdisch', '0', '2');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('französisch', '0', '2');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('ägyptisch', '0', '2');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('3', '1', '3');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1', '0', '3');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('2', '0', '3');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('4', '0', '3');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Pyramiden von Giseh', '1', '4');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Maccu Picchu', '0', '4');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Terracotta-Armee', '0', '4');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Akropolis', '0', '4');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Katze', '1', '5');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Hund', '0', '5');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Kuh', '0', '5');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Pferd', '0', '5');" +

                //Kongo
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('französisch', '1', '6');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('englisch', '0', '6');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('niederländisch', '0', '6');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('deutsch', '0', '6');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('2', '1', '7');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1', '0', '7');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('3', '0', '7');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('4', '0', '7');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Wüste', '1', '8');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Savanne', '0', '8');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Grasland', '0', '8');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Regenwald', '0', '8');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('einem Fluss', '1', '9');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Einem Berg', '0', '9');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('dem Eroberer', '0', '9');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('den Ureinwohnern', '0', '9');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Belgien', '1', '10');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Deutschland', '0', '10');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Frankreich', '0', '10');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('England', '0', '10');" +

                //Südafrika
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Johannesburg', '1', '11');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Kapstadt', '0', '11');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Pretoria', '0', '11');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Bloemfontein', '0', '11');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Vogelarten', '1', '12');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Spinnen', '0', '12');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Gazellen', '0', '12');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Echsen', '0', '12');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('900m', '1', '13');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('700m', '0', '13');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('500m', '0', '13');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('300m', '0', '13');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('11', '1', '14');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('7', '0', '14');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('9', '0', '14');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('13', '0', '14');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1994', '1', '15');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1934', '0', '15');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1964', '0', '15');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1904', '0', '15');" +


                //Indien
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Hindi', '1', '16');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Bengalisch', '0', '16');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Panjabi', '0', '16');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Santali', '0', '16');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('2', '1', '17');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1', '0', '17');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('3', '0', '17');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('4', '0', '17');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Chai', '1', '18');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Kaffee', '0', '18');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Bier', '0', '18');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Mangosaft', '0', '18');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Shampoo', '1', '19');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Bier', '0', '19');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Rad', '0', '19');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Seife', '0', '19');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Die größte Moschee', '0', '20');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Der zweitgrößte Flughafen', '1', '20');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Die größte Universität', '0', '20');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Der zweitgrößte Bahnhof', '0', '20');" +

                //Japan
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('117', '1', '21');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('105', '0', '21');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('110', '0', '21');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('112', '0', '21');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('6852', '1', '22');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('3852', '0', '22');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('5852', '0', '22');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('4852', '0', '22');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Honshu', '1', '23');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Hokkaido', '0', '23');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Shikoku', '0', '23');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Kyushu', '0', '23');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Tokio', '1', '24');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Hokkaido', '0', '24');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Kyoto', '0', '24');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Osaka', '0', '24');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('37', '1', '25');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('35', '0', '25');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('40', '0', '25');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('30', '0', '25');" +

                //Malaysia
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Thailänder', '1', '26');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Malayen', '0', '26');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Inder', '0', '26');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Chinesen', '0', '26');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('88', '1', '27');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('99', '0', '27');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('66', '0', '27');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('77', '0', '27');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Kobras', '1', '28');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Spinnen', '0', '28');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Koalas', '0', '28');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Ameisen', '0', '28');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Islam', '1', '29');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Christentum', '0', '29');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Buddhismus', '0', '29');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Hinduismus', '0', '29');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Tiger', '1', '30');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Koala', '0', '30');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Schlange', '0', '30');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Elefant', '0', '30');" +


                //Deutschland
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Kölner Dom', '1', '31');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Dresdner Frauenkirche', '0', '31');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Brandenburger Tor', '0', '31');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Schloss Neuschwanstein', '0', '31');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Zoo der Welt', '1', '32');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Kreisverkehr Deutschlands', '0', '32');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Fluss Deutschlands', '0', '32');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Stau der Welt', '0', '32');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('100', '0', '33');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('200', '0', '33');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('300', '1', '33');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('400', '0', '33');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Hamburg', '0', '34');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Köln', '0', '34');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Bonn', '0', '34');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Berlin', '1', '34');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1988', '0', '35');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1989', '0', '35');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1990', '1', '35');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1991', '0', '35');" +

                //Frankreich
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1', '1', '36');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('2', '0', '36');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('3', '0', '36');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('4', '0', '36');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Gerechtigkeit', '1', '37');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Freiheit', '0', '37');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Brüderlichkeit', '0', '37');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Gleichheit', '0', '37');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Mont Blanc', '1', '38');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Canigou', '0', '38');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Dôme de Rôchefort', '0', '38');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Dôme de Gôuter', '0', '38');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Wein', '1', '39');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Bier', '0', '39');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Tee', '0', '39');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Kaffee', '0', '39');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Paris', '1', '40');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Nizza', '0', '40');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Bordeaux', '0', '40');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Lyon', '0', '40');" +

                //Niederlande
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Fahrrad', '1', '41');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Auto', '0', '41');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Zug', '0', '41');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Bus', '0', '41');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('25%', '1', '42');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('20%', '0', '42');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('15%', '0', '42');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('10%', '0', '42');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Käse', '1', '43');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Wurst', '0', '43');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Spargel', '0', '43');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Fisch', '0', '43');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Den Haag', '1', '44');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Rotterdam', '0', '44');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Amsterdam', '0', '44');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Groningen', '0', '44');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Hafen der Welt', '1', '45');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Kreisverkehr Europas', '0', '45');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Zoo der Welt', '0', '45');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Fluss Europas', '0', '45');" +

                //USA
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('50', '1', '46');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('16', '0', '46');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('52', '0', '46');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('34', '0', '46');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Washington D.C.', '1', '47');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('New York', '0', '47');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Detroit', '0', '47');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Los Angeles', '0', '47');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('2', '1', '48');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1', '0', '48');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('3', '0', '48');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('4', '0', '48');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Filmindustrie', '1', '49');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Musikindustrie', '0', '49');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Sandstrände', '0', '49');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Inselgruppe', '0', '49');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Indianer', '1', '50');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Amerikaner', '0', '50');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Aborigines', '0', '50');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('First Nation', '0', '50');" +

                //Kanada
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Ottawa', '1', '51');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Vancouver', '0', '51');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Toronto', '0', '51');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Montreal', '0', '51');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1931', '1', '52');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1803', '0', '52');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1704', '0', '52');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('1943', '0', '52');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('größte', '1', '53');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('zweitgrößte', '0', '53');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('meistbewohnte', '0', '53');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('wärmste', '0', '53');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Französisch + Englisch', '1', '54');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Französisch + Deutsch', '0', '54');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Französisch + Deutsch + Englisch', '0', '54');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Englisch + Spanisch', '0', '54');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Ca. Zwei Millionen', '1', '55');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Ca. Zweihunderttausend', '0', '55');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Ca. Fünftausend', '0', '55');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Ca. Vierhundert', '0', '55');" +

                //Mexiko
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Antike Maya Tempel', '1', '56');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Uralte Kriegsschiffe', '0', '56');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Riesige Regenwälder', '0', '56');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Türkisblaue Seen', '0', '56');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Mexiko-Stadt', '1', '57');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Acapulco', '0', '57');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Tabasco', '0', '57');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Monterrey', '0', '57');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Spanisch', '1', '58');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Italienisch', '0', '58');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Portugiesisch', '0', '58');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Mexikanisch', '0', '58');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Nationalgericht', '0', '59');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Strohhut', '1', '59');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Pyramide', '0', '59');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Schnauzbart', '0', '59');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Riesiges Fest feiern', '1', '60');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Fasten', '0', '60');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Wanderung zu einer Grabstätte', '0', '60');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Zu Hause ausruhen', '0', '60');" +

                //Argentinien
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Anconcagua', '1', '61');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Mount Everest', '0', '61');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Makalu', '0', '61');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Kilimandjaro', '0', '61');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Italiener', '1', '62');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Chinesen', '0', '62');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Koreaner', '0', '62');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Südafrikaner', '0', '62');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('in der Hauptstadt', '1', '63');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('an der Küste', '0', '63');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('in den Bergen', '0', '63');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('im Süden', '0', '63');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('alle Klimazonen', '1', '64');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('keine Berge', '0', '64');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('nur Wüste', '0', '64');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('keine großen Städte', '0', '64');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Peso', '1', '65');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Euro', '0', '65');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Lira', '0', '65');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Dollar', '0', '65');" +

                //Brasilien
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Portugiesisch', '1', '66');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Brasilianisch', '0', '66');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Spanisch', '0', '66');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Englisch', '0', '66');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Brasilia', '1', '67');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Rio de Janeiro', '0', '67');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Sao Paulo', '0', '67');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Manaus', '0', '67');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Fußball', '1', '68');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Handball', '0', '68');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Fechten', '0', '68');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Golf', '0', '68');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('wasserreichsten', '1', '69');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('längsten', '0', '69');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('saubersten', '0', '69');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('tiefsten', '0', '69');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Karneval', '1', '70');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Tag der Toten', '0', '70');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Weihnachten', '0', '70');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Ramadan', '0', '70');" +

                //Peru
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Gebirgsstadt', '1', '71');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Großstadt', '0', '71');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Küstenregion', '0', '71');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Gebirgskette', '0', '71');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('771 m', '1', '72');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('883 m', '0', '72');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('550 m', '0', '72');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('338 m', '0', '72');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Savanne', '1', '73');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Gebirge', '0', '73');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Wüste', '0', '73');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Küste', '0', '73');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Vogelarten', '1', '74');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Fischarten', '0', '74');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Inseln', '0', '74');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Seen', '0', '74');" +

                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Westküste', '1', '75');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Gebirge', '0', '75');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Insel', '0', '75');" +
                "INSERT INTO answers(answer_text, is_correct, question_id) VALUES('Wüste', '0', '75');";
    #endregion

    #region Information
    const string INSERT_INFORMATION =
                //Ägypten
                "INSERT INTO information(information_text, country_id) VALUES('Die Hauptstadt Kairo ist mit 22mio Einwohnern die größte Stadt Afrikas und des mittleren Ostens', '1');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Amtssprache Ägyptens ist arabisch.', '1');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Ägypten scheint im Durschnitt 10 Stunden am Tag die Sonne und es ist damit das drittsonnigste Land der Erde', '1');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Pyramiden von Gizeh in Ägypten wurden 2500 v.C.fertig gebaut und sind Weltkulturerbe.', '1');" +
                "INSERT INTO information(information_text, country_id) VALUES('Katzen waren im alten Ägypten heilig. Für das Töten einer Katze erhielt man die Todesstrafe.', '1');" +
                //Kongo
                "INSERT INTO information(information_text, country_id) VALUES('Im Kongo spricht man französisch.', '2');" +
                "INSERT INTO information(information_text, country_id) VALUES('Mit einer Fläche von über 2,3mio Quadratmetern ist der Kongo das flächenmäßig zweitgrößte Land in Afrika.', '2');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Natur im Kongo ist sehr vielfältig. Es gibt Regenwald, Gebirge, Savanne und Grasland ', '2');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Demokratische Republik Kongo wurde nach dem Fluss Kongo benannt, der der zweitgrößte Fluss in Afrika ist.', '2');" +
                "INSERT INTO information(information_text, country_id) VALUES('Der Kongo wurde von Belgien kolonialisiert und erhielt 1960 seine Unabhängigkeit.', '2');" +
                //Südafrika
                "INSERT INTO information(information_text, country_id) VALUES('Südafrika hat 3 Hauptstädte: Pretoria, Kapstadt und Bloemfontein.', '3');" +
                "INSERT INTO information(information_text, country_id) VALUES('Mit 900 verschiedenen Vogelarten leben in Südafrika zehn Prozent aller Vogelarten weltweit.', '3');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Tugela Wasserfälle in Südafrika sind mit 948m die höchsten Wasserfälle Afrikas.', '3');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Südafrika werden 11 Sprachen gesprochen: Zulu, Xhosa, Afrikaans, English, Northern Sotho, Tswana, Southern Sotho, Tsonga, Swazi, Venda, Southern Ndebele.', '3');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Südafrika herrschte fast 50 Jahre lang Rassentrennung zwischen dunkel- und hellhäutigen. Diese wurde 1994 abgeschafft.', '3');" +

                //Indien
                "INSERT INTO information(information_text, country_id) VALUES('In Indien gibt es 22 offizielle Sprachen.Am meisten gesprochen wird Hindi.', '4');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Indien leben nach China die meisten Menschen der Erde.', '4');" +
                "INSERT INTO information(information_text, country_id) VALUES('Das Nationalgetränk in Indien ist Chai (Tee).', '4');" +
                "INSERT INTO information(information_text, country_id) VALUES('Inder haben das Shampoo erfunden.', '4');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Indiens Hauptstadt Delhi ist der zweitgrößte Flughafen der Erde.', '4');" +
                //Japan
                "INSERT INTO information(information_text, country_id) VALUES('Die beiden ältesten Menschen auf der Erde sind Japaner und sind 116 und 117 Jahre alt.', '5');" +
                "INSERT INTO information(information_text, country_id) VALUES('Japan besteht aus 6852 Inseln.', '5');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die größte japanische Insel ist Honshu, gefolgt von Hokkaido, Kyushu und Shikoku.', '5');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Hauptstadt von Japan ist Tokio. Hier leben über 9mio Einwohner.', '5');" +
                "INSERT INTO information(information_text, country_id) VALUES('In der Metropolregion der Hauptstadt leben über 37mio Einwohner.', '5');" +
                //Malaysia
                "INSERT INTO information(information_text, country_id) VALUES('In Malaysia leben Menschen verschiedener Herkünfte zusammen. Malayen, Chinesen und Inder.', '6');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Petrona Towers waren bis 2004 das höchste Gebäude der Erde.Hier gibt es 88 Stockwerke.', '6');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Malaysia lebt die weltgrößte Population von Kobras, den längsten Giftschlangen.', '6');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Hauptreligion ist der Islam (60%). Es gibt auch Buddhisten(20%), Christen(9%) und Hindu(6%)', '6');" +
                "INSERT INTO information(information_text, country_id) VALUES('Das Nationaltier von Malaysia ist der Tiger.', '6');" +

                //Deutschland
                "INSERT INTO information(information_text, country_id) VALUES('Die beliebteste Sehenswürdigkeit ist der Kölner Dom.', '7');" +
                "INSERT INTO information(information_text, country_id) VALUES('Der Zoologische Garten in Berlin ist der größte Zoo der Welt.', '7');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Deutschland werden mehr als 300 Sorten Brot gebacken.', '7');" +
                "INSERT INTO information(information_text, country_id) VALUES('In der Hauptstadt Berlin leben über 3,5mio Einwohner.', '7');" +
                "INSERT INTO information(information_text, country_id) VALUES('Bis 1990 war Deutschland in die BRD und die DDR geteilt und durch eine Mauer getrennt.', '7');" +
                //Frankreich
                "INSERT INTO information(information_text, country_id) VALUES('Frankreich ist mit über 550 000 Quadratkilometern das größte Land Europas.', '8');" +
                "INSERT INTO information(information_text, country_id) VALUES('Das Nationalmotto bedeutet übersetzt: „Freiheit, Gleichheit und Brüderlichkeit“.', '8');" +
                "INSERT INTO information(information_text, country_id) VALUES('Der höchste Berg Europas, der Mont Blanc, befindet sich in den französischen Alpen.', '8');" +
                "INSERT INTO information(information_text, country_id) VALUES('Frankreich ist bekannt für seinen leckeren Wein.', '8');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Hauptstadt von Frankreich ist Paris.', '8');" +
                //Niederlande
                "INSERT INTO information(information_text, country_id) VALUES('In den Niederlanden gibt es mehr Fahrräder als Einwohner.', '9');" +
                "INSERT INTO information(information_text, country_id) VALUES('26% des Landes liegen unter dem Meeresspiegel.', '9');" +
                "INSERT INTO information(information_text, country_id) VALUES('Niederländer essen im Schnitt 17kg Käse im Jahr.', '9');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Hauptstadt der Niederlande ist Amsterdam.Der Regierungssitz befindet sich in Den Haag.', '9');" +
                "INSERT INTO information(information_text, country_id) VALUES('Rotterdam besitzt den größten Hafen der Welt.', '9');" +

                //USA
                "INSERT INTO information(information_text, country_id) VALUES('Die 50 Sterne auf der Flagge stehen für die einzelnen Staaten.', '10');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Hauptstadt von den USA ist Washington D.C.', '10');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die USA grenzt nur zwei andere Länder. Kanada und Mexiko.', '10');" +
                "INSERT INTO information(information_text, country_id) VALUES('In den USA befindet sich die große Filmindustrie Hollywood. Hier entstehen die meisten Kinofilme.', '10');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Ureinwohner der USA werden als Indianer bezeichnet.', '10');" +
                //Kanada
                "INSERT INTO information(information_text, country_id) VALUES('Die Hauptstadt von Kanada ist Ottawa in der Provinz Ontario.', '11');" +
                "INSERT INTO information(information_text, country_id) VALUES('Seit 1931 ist Kanada ein eigenständiges und unabhängiges Land.', '11');" +
                "INSERT INTO information(information_text, country_id) VALUES('Kanada ist flächenmäßig das zweitgrößte Land hat aber vergleichsweise wenige Einwohner.', '11');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Amtssprachen in Kanada sind Englisch und Französisch.', '11');" +
                "INSERT INTO information(information_text, country_id) VALUES('Kanada ist bekannt für seine landschaftliche Vielfalt.Es gibt sehr hohe Gebirge, kalte Winterlandschaften und rund zwei Millionen Seen.', '11');" +
                //Mexico
                "INSERT INTO information(information_text, country_id) VALUES('Früher lebten im heutigen Mexiko verschiedene Völker. Z.B.die Maya und die Azteken, von welchen es heute noch riesige Tempel und Pyramiden gibt.', '12');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Hauptstadt von Mexiko wurde nach dem Land selbst benannt.Sie heißt Mexiko-Stadt. ', '12');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Mexiko hat keine eigene Sprache. Man spricht hier Spanisch.', '12');" +
                "INSERT INTO information(information_text, country_id) VALUES('Der Sombrero ist ein sehr großer runder Strohhut, der in Mexiko besonders beliebt ist.', '12');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Mexikaner feiern sehr gerne große traditionelle Feste.Unter anderem den Tag der Toten.', '12');" +

                //Argentinien
                "INSERT INTO information(information_text, country_id) VALUES('Der höchste Berg Amerikas steht in Argentinien.Der Aconcagua ist ca. 6900 Meter hoch.', '13');" +
                "INSERT INTO information(information_text, country_id) VALUES('Am Anfang des 20. Jahrhunderts wanderten eine große Menge Europäer nach Argentinien ein.Hauptsächlich Italiener und Spanier.', '13');" +
                "INSERT INTO information(information_text, country_id) VALUES('In der Hauptstadt Buenos Aires lebt ca.jeder dritte Argentinier.', '13');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Argentinien sind nahezu alle Klimazonen vorzufinden. Neben Tropenwäldern und Wüsten gibt es im Süden auch sehr kalte Gebiete.', '13');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Argentinien bezahlt man mit dem argentinischen Peso.', '13');" +
                //Brasilien
                "INSERT INTO information(information_text, country_id) VALUES('Brasilien ist das Einzige Land in Südamerika, in dem man Portugiesisch spricht. Dies ist die einzige Sprache in Brasilien.', '14');" +
                "INSERT INTO information(information_text, country_id) VALUES('Rio de Janeiro war lange Zeit Hauptstadt Brasiliens.Mittlerweile ist Brasilia, die extra gebaut wurde um als Hauptstadt zu dienen.', '14');" +
                "INSERT INTO information(information_text, country_id) VALUES('Der Nationalsport des Landes ist Fußball.Brasiliens Nationalmannschaft zählt zu den erfolgreichsten der Welt.', '14');" +
                "INSERT INTO information(information_text, country_id) VALUES('Durch Brasilien fließt der wasserreichste Fluss der Erde; der Amazonas.Um ihn herum befindet sich ebenfalls der größte Regenwald der Welt.', '14');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Brasilien feiert man jedes Jahr den Karneval. Er zählt zu den größten Festen der Welt und wird in der Stadt Rio de Janeiro ausgetragen.', '14');" +
                //Peru
                "INSERT INTO information(information_text, country_id) VALUES('Vor ca. 500 Jahren erbauten die damaligen Bewohner Perus die Gebirgsstadt Machu Picchu.Sie ist heute noch gut erhalten.', '15');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Peru gibt es einen der höchsten Wasserfälle der Welt.Die Gocta Fälle sind 771 Meter hoch.', '15');" +
                "INSERT INTO information(information_text, country_id) VALUES('Peru besteht aus drei verschiedenen Landschaftszonen. Der Küste, Der Wüste und dem Gebirge.Das Klima unterscheidet sich hier sehr stark.', '15');" +
                "INSERT INTO information(information_text, country_id) VALUES('In Peru wurden über 1800 verschiedene Vogelarten entdeckt.', '15');" +
                "INSERT INTO information(information_text, country_id) VALUES('Die Hauptstadt von Peru ist Lima und liegt an der Westküste des Kontinents.', '15');";
    #endregion

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

    public static Continent GetContinentByCountry(string countryString)
    {
        Continent result;
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            using (SqliteCommand command = new SqliteCommand($"SELECT * FROM continents AS continent " +
                "JOIN countries AS country ON country.continent_id=continent.id " +
                $"WHERE country.name='{countryString}';", connection))
            {
                command.Connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new Continent(Convert.ToInt32(reader["id"]), reader["name"].ToString());
                        return result;
                    }
                }
            }
        }
        return null;
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