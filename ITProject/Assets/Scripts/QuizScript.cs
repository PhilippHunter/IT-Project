using Assets.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizScript : MonoBehaviour
{
    TextMeshProUGUI m_Text;
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        List<Question> uff = SceneSwitcher.questions;
        m_Text.text = uff[0].Text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
