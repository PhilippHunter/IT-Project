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
        SceneManager.LoadScene("ContinentSelection");
    }

    public void OpenOnboarding()
    {
      SceneManager.LoadScene("OnBoarding");
    }
    private void Start()
    {
        SqliteScript.CreateDB();
    }
}
