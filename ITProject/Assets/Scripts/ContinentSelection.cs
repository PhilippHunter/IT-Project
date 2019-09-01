using Assets.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ContinentSelection : MonoBehaviour
{
    public static List<Country> countries;   
    public void showCountries()
    {   
        //Collecting all countries for clicked continent
        countries = SqliteScript.GetCountriesByContinent(EventSystem.current.currentSelectedGameObject.name);
        SceneManager.LoadScene("CountrySelection");
    }
}
