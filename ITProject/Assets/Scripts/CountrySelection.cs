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
        //if (ContinentSelection.countries != null)
        //{
        //    button.GetComponentInChildren<Text>().text = ContinentSelection.countries[0].Name;

        //    //Making copies of sample object for all countries in list
        //    for (int i = 1; i < ContinentSelection.countries.Count; i++)
        //    {
        //        GameObject newButton = Instantiate(button) as GameObject;
        //        //adding new position to object
        //        newButton.transform.position = new Vector3(0, 470 - i * 150, -173.25f);
        //        newButton.transform.SetParent(menu.transform, false);
        //        newButton.GetComponentInChildren<Text>().text = ContinentSelection.countries[i].Name;
        //    }
        //}
    }

    public void countrySelected()
    {
        CountryStartPage.countryName= EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene("CountryStartPage");
    }
}
