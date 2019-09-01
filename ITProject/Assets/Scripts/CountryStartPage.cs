using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        }
        else
            button.GetComponentInChildren<Text>().text = countryName;
    }
}
