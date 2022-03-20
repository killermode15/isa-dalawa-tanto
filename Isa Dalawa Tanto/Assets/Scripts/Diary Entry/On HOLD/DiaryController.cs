using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine.Events;

public class DiaryController : BaseController
{
    public static DiaryController Instance => instance;
    private static DiaryController instance;

    private DiaryView dv;
    private DiaryModel dm;

    public UnityEvent onCompleteEntry;
    public UnityEvent onFailEntry;

    private void Start()
    {
        instance = this;
        SetupController<DiaryView, DiaryModel>();
        dv = view as DiaryView;
        dm = model as DiaryModel;
        SetupDiary();
    }

    public void SetupDiary()
    {
        dv.SetupEntry(dm.GetDiaryEntry(), dm.pages, 0);
    }

    public void NextPage()
    {
        SaveAnswer(dv.GetAnswerFieldHandlers());

        int _currentPage = dm.pages.IndexOf(dm.CurrentPage);
        _currentPage += 1;

        Mathf.Clamp(_currentPage, 0, dm.pages.Count);
        dv.SetPage(dm.GetDiaryEntry(), dm.pages[_currentPage]);
        dm.CurrentPage = dm.pages[_currentPage];

        if (dv.GetAnswerFieldHandlers().Count > 0)
            dv.SetExistingAnswers(dm.createdAnswers);
    }

    public void PreviousPage()
    {
        SaveAnswer(dv.GetAnswerFieldHandlers());

        int _currentPage = dm.pages.IndexOf(dm.CurrentPage);
        _currentPage -= 1;

        Mathf.Clamp(_currentPage, 0, dm.pages.Count);
        dv.SetPage(dm.GetDiaryEntry(), dm.pages[_currentPage]);
        dm.CurrentPage = dm.pages[_currentPage];

        if (dv.GetAnswerFieldHandlers().Count > 0)
            dv.SetExistingAnswers(dm.createdAnswers);
    }

    public void FinishEntry(GameObject objectToActivate)
    {
        if (!dv.ValidateEntries(dm.createdAnswers, dm.correctAsnwers))
        {
            Debug.Log("There are incorrect answers");
            dv.ReduceHealth(1);

            if(dv.GetLives() == 0)
            {
                Debug.Log("Player failed, reloading current stage");
                onFailEntry?.Invoke();
                SceneHandler.Instance.SwitchScene(SceneHandler.Instance.GetCurrentSceneIndex());
            }

            return;
        }
    
        objectToActivate.SetActive(true);
    }

    private void SaveAnswer(List<AnswerFieldHandler> answerfields)
    {
        for (int i = 0; i < answerfields.Count; i++)
        {
            if (dm.createdAnswers.Count < answerfields.Count)
            {
                dm.createdAnswers.Add(answerfields[i].answerField.text);
            }

            dm.createdAnswers[i] = answerfields[i].answerField.text;
        }
    }
}
