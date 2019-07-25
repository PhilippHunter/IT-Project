using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Model;
using Vuforia;
using UnityEngine.UI;
using System;

public class SceneSwitcher : MonoBehaviour
{
    public static SortedDictionary<Question, List<Answer>> QuestionsMap { get; set; }
    public static List<Country> Countries { get; set; }
    public void startQuiz(string country)
    {
        //get all questions for the current country
        List<Question> quests = SqliteScript.GetQuestionsByCountry("Germany");

        //get the answers for every question and attach it to the question in a map
        QuestionsMap = new SortedDictionary<Question, List<Answer>>();
        foreach (Question q in quests)
        {
            List<Answer> answers = SqliteScript.GetAnswersByQuestionId(q.ID);
            if (answers.Count != 0)
                QuestionsMap.Add(q, answers);
        }

        SceneManager.LoadScene("Quiz");
    }

    public void getCountryInfo(string country)
    {
        //make database query here to get correct content for country

        Debug.Log("Country: " + country);
        SceneManager.LoadScene("Learn");
    }
}
