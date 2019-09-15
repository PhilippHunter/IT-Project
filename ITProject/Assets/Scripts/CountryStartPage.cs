using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountryStartPage : MonoBehaviour
{
    public static string countryName = "";
    void Start()
    {
        //Find Sample Objects
        GameObject button = GameObject.Find("CountryName");
        if (SceneSwitcher.currentCountryName != "")
        {
            button.GetComponentInChildren<Text>().text = SceneSwitcher.currentCountryName;
            countryName = SceneSwitcher.currentCountryName;
        }
        else
            button.GetComponentInChildren<Text>().text = countryName;
    }

    public void changeToInformationDisplay()
    {
        SceneManager.LoadScene("CountryDetailPage");
    }
}
