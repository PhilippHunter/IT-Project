using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Model;

public class SceneSwitcher : MonoBehaviour
{
  public void startQuiz(string country)
  {
    //make database query here to get correct content for country
    List<Question> questions = SqliteScript.GetQuestionsByCountry(country);

    //SceneManager.LoadScene("Menu");
    foreach(Question q in questions) 
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
