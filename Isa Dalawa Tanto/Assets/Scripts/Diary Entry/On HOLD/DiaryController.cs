using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiaryController : BaseController
{
    public static DiaryController Instance => instance;
    private static DiaryController instance;

    private DiaryView dv;
    private DiaryModel dm;

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
        SecureAnswer(dv.GetAnswerFieldHandlers());

        int _currentPage = dm.pages.IndexOf(dm.CurrentPage);
        _currentPage += 1;

        Mathf.Clamp(_currentPage, 0, dm.pages.Count);
        dv.SetPage(dm.GetDiaryEntry(), dm.pages[_currentPage]);
        dm.CurrentPage = dm.pages[_currentPage];
        //set answers again
    }

    public void PreviousPage()
    {
        SecureAnswer(dv.GetAnswerFieldHandlers());

        int _currentPage = dm.pages.IndexOf(dm.CurrentPage);
        _currentPage -= 1;

        Mathf.Clamp(_currentPage, 0, dm.pages.Count);
        dv.SetPage(dm.GetDiaryEntry(), dm.pages[_currentPage]);
        dm.CurrentPage = dm.pages[_currentPage];
        //set answers again
    }

    public void FinishEntry()
    {
        if (!dv.ValidateEntries(dv.GetAnswerFieldHandlers(), dm.correctAsnwers)) 
        {
            Debug.Log("There are incorrect answers");
            dv.ReduceHealth(1);
            return;
        }

        //affirm that the player has answered correctly
        Debug.Log("All answers are correct");

        // SceneManager.SetActiveScene();
    }

    private void SecureAnswer(List<AnswerFieldHandler> answerfields)
    {
        for (int i = 0; i < answerfields.Count; i++)
        {
            if (answerfields[i].answerField.text == null)
                answerfields[i].answerField.text = "";

            dm.createdAnswers.Add(answerfields[i].answerField.text);
        }
    }
}
