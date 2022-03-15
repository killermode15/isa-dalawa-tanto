using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    public static FadeUI Instance => instance;

    private static FadeUI instance;

    [SerializeField] private Image imageToFade;

    public float Duration => duration;

    [SerializeField] private float duration;

    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        //imageToFade.color = Color.clear;
    }

    public void Fade(bool fade = true)
    {
        StartCoroutine(FadeToBlack(fade));
    }

    public IEnumerator FadeToBlack(bool fade = true)
    {
        Color startingColor = fade ? imageToFade.color : Color.black; 
        Color targetColor = fade ? Color.black : Color.clear;

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
