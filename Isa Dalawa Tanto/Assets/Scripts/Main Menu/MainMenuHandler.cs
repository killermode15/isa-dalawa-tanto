using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(FadeBeforeStart());
    }

    public void ExitGame()
    {
        StartCoroutine(FadeBeforeExit());
    }

    private IEnumerator FadeBeforeExit()
    {
        FadeUI.Instance.Fade();
        yield return new WaitForSeconds(FadeUI.Instance.Duration);
#if UNITY_EDITOR
        Debug.Log("Exit");
#else
        Application.Quit();
#endif
    }

    private IEnumerator FadeBeforeStart()
    {
        FadeUI.Instance.Fade();
        yield return new WaitForSeconds(FadeUI.Instance.Duration);
        SceneHandler.Instance.SwitchScene(1);
    }
}
