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
    public Button answer1, answer2, answer3, answer4;
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

        //set question text
        questionText.text = question.Text;

        /*set answer texts*/
        /*tryed to shuffel order of answers so they're always different*/
        //var shuffledAnswers = answers.OrderBy(item => UnityEngine.Random.Range(0,answers.Count));

        answer1.GetComponentInChildren<TextMeshProUGUI>().text = answers[0].Text;
        answer2.GetComponentInChildren<TextMeshProUGUI>().text = answers[1].Text;
        answer3.GetComponentInChildren<TextMeshProUGUI>().text = answers[2].Text;
        answer4.GetComponentInChildren<TextMeshProUGUI>().text = answers[3].Text;

        questionCounter++;
    }
}
