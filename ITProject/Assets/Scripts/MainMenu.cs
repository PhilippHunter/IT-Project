﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame(){
        SqliteScript.GetQuestionByCountry("Germany");
      SceneManager.LoadScene("AR");
   }

   public void OpenLearningScene() {
      SceneManager.LoadScene("Learn");
   }
}