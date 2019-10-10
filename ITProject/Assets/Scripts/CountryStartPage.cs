using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Model;

public class CountryStartPage : MonoBehaviour
{
    public static string countryName = "";
    void Start()
    {
        //Find Sample Objects
        GameObject button = GameObject.Find("CountryName");
        GameObject menu = GameObject.Find("CountryMenu");

        if (SceneSwitcher.currentCountryName != "")
        {
            button.GetComponentInChildren<Text>().text = SceneSwitcher.currentCountryName.ToUpper();
            countryName = SceneSwitcher.currentCountryName;

            //reset so that new countries can be selected
            SceneSwitcher.currentCountryName = "";
        }
        else
            button.GetComponentInChildren<Text>().text = countryName.ToUpper();

        Continent currentContinent = SqliteScript.GetContinentByCountry(countryName);
        Debug.Log(currentContinent.Name);

        //finding inactive objects like this
        Transform[] trs = menu.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in trs)
        {
            if(t.name == currentContinent.Name)
            {
                t.gameObject.SetActive(true);
            }
        }
    }

    public void changeToInformationDisplay()
    {
        SceneManager.LoadScene("CountryDetailPage");
    }
}
