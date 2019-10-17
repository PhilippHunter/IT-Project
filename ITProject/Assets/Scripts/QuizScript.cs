using Assets.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class QuizScript : MonoBehaviour
{
    public TextMeshProUGUI countryName;
    public TextMeshProUGUI questionText;
    public Button[] buttons;
    int questionCounter = 0;
    int score = 0;
    public GameObject winScreen, failScreen;
    public TextMeshProUGUI winCountryTextField, failCountryTextField;

    // Start is called before the first frame update
    void Start()
    {
        InitializeQuizSection();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeQuizSection()
    {
        if (questionCounter < SceneSwitcher.QuestionsMap.Count)
        {
            /*Set name of Quiz*/
            countryName.text = SceneSwitcher.currentCountryName;

            Question question = SceneSwitcher.QuestionsMap.Keys.ElementAt(questionCounter);
            List<Answer> answers = SceneSwitcher.QuestionsMap.Values.ElementAt(questionCounter);

            /*set question text*/
            questionText.text = question.Text;

            /*set answer texts*/
            var shuffledAnswers = answers.OrderBy(item => UnityEngine.Random.Range(0, answers.Count))
                .ToList();

            buttons[0].GetComponentInChildren<Text>().text = shuffledAnswers[0].Text;
            buttons[1].GetComponentInChildren<Text>().text = shuffledAnswers[1].Text;
            buttons[2].GetComponentInChildren<Text>().text = shuffledAnswers[2].Text;
            buttons[3].GetComponentInChildren<Text>().text = shuffledAnswers[3].Text;

            /*set click listeners for buttons considering their correctness*/
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].onClick.RemoveAllListeners();
                if (shuffledAnswers[i].IsCorrectAnswer)
                    buttons[i].onClick.AddListener(OnClickRightAnswer);
                else
                    buttons[i].onClick.AddListener(OnClickWrongAnswer);
            }

            questionCounter++;
        }
        else
        {
            /*no more questions -> finish screen*/
            ShowResults();
        }
    }

    void ShowResults()
    {
        if (score == SceneSwitcher.QuestionsMap.Count)
        {
            //fill screen with data from current quiz (country name)
            winCountryTextField.text = SceneSwitcher.currentCountryName;

            //show screen
            winScreen.SetActive(true);

            //update completion state in database
            SqliteScript.SetQuizCompleted(SceneSwitcher.currentCountryName);
        }
        else
        {
            //TODO create loose screen in scene
            failCountryTextField.text = SceneSwitcher.currentCountryName;

            //show screen
            failScreen.SetActive(true);
        }
    }

    void OnClickRightAnswer()
    {
        score++;
        /*load next quiz step*/
        InitializeQuizSection();
    }

    void OnClickWrongAnswer()
    {
        /*load next quiz step*/
        InitializeQuizSection();
    }
}
