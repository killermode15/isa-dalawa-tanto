using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerFieldHandler : MonoBehaviour
{
    public TMP_InputField answerField;

    public int ID { get => id; set => id = value; }
    public Page Page { get => page; set => page = value; }
    [SerializeField] private Color correctAnswerColor;
    [SerializeField] private Color incorrectAnswerColor;
    [SerializeField] private float fadeDuration;

    private Page page;
    private int id;

    public Answer CreateAnswer()
    {
        if(page == null)
        {
            Debug.LogError("page is null");
            return null;
        }

        if (answerField.text == string.Empty)
        {
            Debug.LogError("There is an empty answerfield");
            return null;
        }

        return new Answer(id, page, answerField.text);
    }

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
