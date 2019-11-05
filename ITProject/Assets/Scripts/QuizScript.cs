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
    private Transform currentQuestionDisplay;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        Continent currentContinent = SqliteScript.GetContinentByCountry(SceneSwitcher.currentCountryName);
        Debug.Log(currentContinent.Name);

        //finding inactive objects like this
        Transform[] trs = canvas.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == currentContinent.Name)
            {
                currentQuestionDisplay = t;

                //showing the display screen with correct colors
                currentQuestionDisplay.gameObject.SetActive(true);

                //setting the correct variables for output
                foreach(TextMeshProUGUI currentObject in t.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (currentObject.name.Equals("QuizName")) countryName = currentObject;
                    else if (currentObject.name.Equals("QuestionText")) questionText = currentObject;
                }

                for (int i = 0; i < t.GetComponentsInChildren<Button>().Count(); i++) buttons[i] = t.GetComponentsInChildren<Button>()[i];
            }            
        }
        InitializeQuizSection();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeQuizSection()
    {
        if (questionCounter < SceneSwitcher.Questions.Count)
        {
            Question question = SceneSwitcher.Questions[questionCounter];
            List<Answer> answers = question.Answers;

            /*Set name of Quiz*/
            countryName.text = SceneSwitcher.currentCountryName;

            /*set question text*/
            questionText.text = question.Text;

            /*set answer texts*/
            buttons[0].GetComponentInChildren<Text>().text = question.Answers[0].Text;
            buttons[1].GetComponentInChildren<Text>().text = question.Answers[1].Text;
            buttons[2].GetComponentInChildren<Text>().text = question.Answers[2].Text;
            buttons[3].GetComponentInChildren<Text>().text = question.Answers[3].Text;

            /*set click listeners for buttons considering their correctness*/
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].onClick.RemoveAllListeners();
                if (question.Answers[i].IsCorrectAnswer)
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
        if (score == SceneSwitcher.Questions.Count)
        {
            //fill screen with data from current quiz (country name)
            winCountryTextField.text = SceneSwitcher.currentCountryName;

            //hide display screen
            currentQuestionDisplay.gameObject.SetActive(false);

            //show win screen
            winScreen.SetActive(true);

            //update completion state in database
            SqliteScript.SetQuizCompleted(SceneSwitcher.currentCountryName);
        }
        else
        {
            //TODO create loose screen in scene
            failCountryTextField.text = SceneSwitcher.currentCountryName;

            //hide display screen
            currentQuestionDisplay.gameObject.SetActive(false);

            //show fail screen
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
