using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance
    {
        get => GameObject.FindObjectOfType<SceneHandler>();
    }

    private FadeUI fadeUIscript;

    // Start is called before the first frame update
    void Start()
    {
        fadeUIscript = FadeUI.Instance;
        fadeUIscript.Fade(false);
    }


    public void SwitchScene(int scnIdx)
    {
        StartCoroutine(FadeBeforeSwitch(scnIdx));
    }

    private IEnumerator FadeBeforeSwitch(int scnIdx)
    {
        fadeUIscript.Fade();
        yield return new WaitForSeconds(fadeUIscript.Duration);
        SceneManager.LoadScene(scnIdx);
    }
}
