using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Model;
using Vuforia;
using UnityEngine.UI;
using System;

public class SceneSwitcher : MonoBehaviour
{
    public static List<Question> questions { get; set; }
    public static List<Country> countries { get; set; }
    public void startQuiz(string country)
    {
        //make database query here to get correct content for country
        questions = SqliteScript.GetQuestionsByCountry("Germany");

        SceneManager.LoadScene("Quiz");
        foreach (Question q in questions)
        {
            Debug.Log(q.Text);
        }
    }

    public void getCountryInfo(string country)
    {
        //make database query here to get correct content for country

        Debug.Log("Country: " + country);
        SceneManager.LoadScene("Learn");
    }
}
