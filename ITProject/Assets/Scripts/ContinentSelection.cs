using Assets.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ContinentSelection : MonoBehaviour
{
    public static List<Country> countries;
    public static string currentContinent;
    public void showCountries()
    {
        //Collecting all countries for clicked continent
        currentContinent = EventSystem.current.currentSelectedGameObject.name;
        countries = SqliteScript.GetCountriesByContinent(currentContinent);
        SceneManager.LoadScene(5);
    }

    public void switchToCountries()
    {
        SceneManager.LoadScene(5);
    }
}
