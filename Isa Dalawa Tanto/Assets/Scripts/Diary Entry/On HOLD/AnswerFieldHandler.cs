using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerFieldHandler : MonoBehaviour
{
    public TMP_InputField answerField;

    public int ID { get { return id; } set { id = value; } }

    private int id;

    public bool ValidateAnswer(string answer)
    {
        if(answerField.text == answer) return true;

        return false;
    }
}
