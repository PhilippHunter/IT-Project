using Assets.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountrySelection : MonoBehaviour { 
    private void Start()
    {
        //Find Sample Objects
        GameObject menu = GameObject.Find("Menu");

        for(int i = 0; i< ContinentSelection.countries.Count; i++)
        {
            menu.transform.GetChild(i).name = ContinentSelection.countries[i].Name;
            menu.transform.GetChild(i).GetComponentInChildren<Text>().text = ContinentSelection.countries[i].Name.ToUpper();
        }
    }

    public void countrySelected()
    {
        CountryStartPage.countryName= EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene("CountryStartPage");
    }
}
