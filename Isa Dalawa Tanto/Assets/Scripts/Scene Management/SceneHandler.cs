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

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void SwitchScene(int scnIdx)
    {
        StartCoroutine(FadeBeforeSwitch(scnIdx));
    }

    private IEnumerator FadeBeforeSwitch(int scnIdx)
    {
        fadeUIscript.Fade(true);
        yield return new WaitForSeconds(fadeUIscript.Duration);
        SceneManager.LoadScene(scnIdx);
    }
}
