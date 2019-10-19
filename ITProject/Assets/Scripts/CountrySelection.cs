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
        GameObject canvas = GameObject.Find("Canvas");

 

        //finding inactive objects like this
        Transform[] trs = canvas.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == ContinentSelection.currentContinent)
            {
                t.gameObject.SetActive(true);
                Transform menu = t.Find("Menu");

                for (int i = 0; i < ContinentSelection.countries.Count; i++)
                {
                    menu.GetChild(i).name = ContinentSelection.countries[i].Name;
                    menu.GetChild(i).GetComponentInChildren<Text>().text = ContinentSelection.countries[i].Name.ToUpper();
                }
            }
        }
    }

    public void countrySelected()
    {
        CountryStartPage.countryName= EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene("CountryStartPage");
    }
}
