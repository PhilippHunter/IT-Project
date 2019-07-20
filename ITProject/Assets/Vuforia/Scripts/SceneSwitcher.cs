using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void startQuiz(string country)
    {
        //make database query here to get correct content for country
        
        SceneManager.LoadScene("Menu");
        //GetQuestionByCountry()
        Debug.Log("Country: " + country);
    }

    public void getCountryInfo(string country)
    {
        //make database query here to get correct content for country


        Debug.Log("Country: " + country);
        SceneManager.LoadScene("Learn");
    }
}
