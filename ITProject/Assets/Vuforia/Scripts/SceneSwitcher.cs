using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Model;
using System.Linq;
using Vuforia;
using UnityEngine.UI;
using System;

public class SceneSwitcher : MonoBehaviour
{
    public static List<Question> Questions { get; set; }
    public static List<Country> Countries { get; set; }

    public static string currentCountryName = "";

    public static bool fromAR = false;

    public void startQuiz(string country)
    {
        //reset to empty
        currentCountryName = country;

        //get all questions for the current country and shuffle them so the order is always different
        Questions = SqliteScript.GetQuestionsByCountry(country);
        Questions = Questions.OrderBy(item => UnityEngine.Random.Range(0, Questions.Count))
                             .ToList();

        //get the answers for each question and attach them as a reference list
        foreach (Question q in Questions)
        {
            q.Answers = SqliteScript.GetAnswersByQuestionId(q.ID);
            q.Answers = q.Answers.OrderBy(item => UnityEngine.Random.Range(0, q.Answers.Count))
                                 .ToList();
        }

        SceneManager.LoadScene("Quiz");
    }

    public void getCountryInfo(string country)
    {
        //make database query here to get correct content for country

        Debug.Log("Country: " + country);
        //save current country to get country from other script
        currentCountryName = country;
        SceneManager.LoadScene("CountryStartPage");

    }

    public void LoadPreviousScene()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "ContinentSelection":
                SceneManager.LoadScene("Menu");
                break;

            case "CountrySelection":
                SceneManager.LoadScene("ContinentSelection");
                break;

            case "CountryStartPage":
                if (fromAR)
                {
                    SceneManager.LoadScene("AR");
                    fromAR = false;
                }
                else
                    SceneManager.LoadScene("CountrySelection");
                break;

            case "CountryDetailPage":
                SceneManager.LoadScene("CountryStartPage");
                break;

            case "AR":
                SceneManager.LoadScene("Menu");
                break;

            case "Quiz":
                SceneManager.LoadScene("AR");
                break;

            case "OnBoarding":
                SceneManager.LoadScene("Menu");
                break;

            default:
                SceneManager.LoadScene("Menu");
                break;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            this.LoadPreviousScene();
    }
}
