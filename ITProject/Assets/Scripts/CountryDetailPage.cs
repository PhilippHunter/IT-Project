using Assets.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountryDetailPage : MonoBehaviour
{
    void Start()
    {
        List<Information> information = new List<Information>();
        if (CountryStartPage.countryName != null)
        {
            information = SqliteScript.GetInformationByCountry(CountryStartPage.countryName);

            //there are always 5 pieces of information for each country
            for (int i = 0; i < information.Count; i++)
            {
                transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = information[i].Text;
            }
        }
    }

    public void LoadARScene()
    {
        SceneManager.LoadScene("AR");
    }
}
