using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Assets.Model;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("AR");
    }

    public void OpenLearningScene()
    {
        List<Country> countries = SqliteScript.GetCountriesByContinent("Europe");
        foreach (Country c in countries)
        {
            Debug.Log(c.Name);
        }

        SceneManager.LoadScene("Learn");
    }
}
