using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Model;
using Vuforia;
using UnityEngine.UI;
using System;

public class SceneSwitcher : MonoBehaviour
{
    public static List<Question> Questions { get; set; }
    public static Dictionary<Question, List<Answer>> QuestionsMap { get; set; }
    public static List<Country> Countries { get; set; }
    public void startQuiz(string country)
    {
        Questions = SqliteScript.GetQuestionsByCountry("Germany");

        /*----EXPERIMENTAL----*/
        //make database query here to get correct content for country
        //List<Question> quests = SqliteScript.GetQuestionsByCountry("Germany");
        //foreach (Question q in quests)
        //{
        //    List<Answer> answers = SqliteScript.GetAnswersByQuestionId(q.ID);
        //    QuestionsMap.Add(q, answers);
        //    Debug.Log(QuestionsMap[q]);
        //}

        SceneManager.LoadScene("Quiz");
    }

    public void getCountryInfo(string country)
    {
        //make database query here to get correct content for country

        Debug.Log("Country: " + country);
        SceneManager.LoadScene("Learn");
    }
}
