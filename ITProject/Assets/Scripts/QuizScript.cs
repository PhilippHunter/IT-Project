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
    public TextMeshProUGUI questionText;
    public Button[] buttons;
    public int questionCounter = 0;

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
        Question question = SceneSwitcher.QuestionsMap.Keys.ElementAt(questionCounter);
        List<Answer> answers = SceneSwitcher.QuestionsMap.Values.ElementAt(questionCounter);
        //TextMeshProUGUI button1 = answer1.GetComponent<TextMeshProUGUI>();
        //TextMeshProUGUI button2 = answer2.GetComponent<TextMeshProUGUI>();
        //TextMeshProUGUI button3 = answer3.GetComponent<TextMeshProUGUI>();
        //TextMeshProUGUI button4 = answer4.GetComponent<TextMeshProUGUI>();

        /*set question text*/
        questionText.text = question.Text;

        /*set answer texts*/
        /*we need to pick 4 random answers from the answer-set (can be more than 4 per question)*/
        /*tryed to shuffel order of answers so they're random*/
        //var shuffledAnswers = answers.OrderBy(item => UnityEngine.Random.Range(0,answers.Count));

        buttons[0].GetComponentInChildren<Text>().text = answers[0].Text;
        buttons[1].GetComponentInChildren<Text>().text = answers[1].Text;
        buttons[2].GetComponentInChildren<Text>().text = answers[2].Text;
        buttons[3].GetComponentInChildren<Text>().text = answers[3].Text;

        /*set click listeners for buttons considering their correctness*/
        for (int i = 0; i < buttons.Length; i++)
        {
            if (answers[i].IsCorrectAnswer)
                buttons[i].onClick.AddListener(OnClickRightAnswer);
            else
                buttons[i].onClick.AddListener(OnClickWrongAnswer);
        }

        questionCounter++;
    }

    public void OnClickRightAnswer()
    {
        /*load next quiz step*/
        /*increment some score counter*/
        InitializeQuizSection();
    }

    public void OnClickWrongAnswer()
    {
        /*load next quiz step*/
        InitializeQuizSection();
    }
}
