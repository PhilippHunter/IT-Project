using Assets.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizScript : MonoBehaviour
{
    private TextMeshProUGUI m_Text;

    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        string allQuestions = string.Join("\n\n", SceneSwitcher.Questions);
        m_Text.text = allQuestions;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
