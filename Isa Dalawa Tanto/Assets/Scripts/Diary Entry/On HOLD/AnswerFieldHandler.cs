using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerFieldHandler : MonoBehaviour
{
    public TMP_InputField answerField;

    public int ID { get { return id; } set { id = value; } }
    [SerializeField] private Color correctAnswerColor;
    [SerializeField] private Color incorrectAnswerColor;
    [SerializeField] private float fadeDuration;
    private int id;

    public bool ValidateAnswer(string answer)
    {
        if (answerField.text == answer)
        {
            ReactColor_CR(answerField.image, correctAnswerColor, fadeDuration);
            return true;
        }

        ReactColor_CR(answerField.image, incorrectAnswerColor, fadeDuration);
        return false;
    }

    public IEnumerator ReactColor_CR(Image imageToFade, Color fadeTo, float duration)
    {
        Color startingColor = imageToFade.color;
        Color targetColor = fadeTo;

        float currDuration = duration;
        float perc = 1 - currDuration / duration;

        while (perc < 1)
        {
            currDuration -= Time.deltaTime;
            perc = 1 - currDuration / duration;

            imageToFade.color = Color.Lerp(startingColor, targetColor, perc);

            yield return new WaitForEndOfFrame();
        }
    }
}
