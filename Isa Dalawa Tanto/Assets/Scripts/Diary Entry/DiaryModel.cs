using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryModel : BaseModel
{
    public int currentEntryID;
    public DiaryDatabase DiaryDatabase => diaryDatabase;
    public List<Page> pages;
    public List<string> correctAsnwers;
    public List<Answer> createdAnswers;
    public Page CurrentPage { get { return currentPage; } set { currentPage = value; } }
    [SerializeField] private DiaryDatabase diaryDatabase;
    [SerializeField] private DiaryContent currentContent;
    private Page currentPage;

    public Page GetPage(int id) => pages[id];
    // public string GetAnswer(int id) => correctAsnwers[id];

    private void Awake()
    {
        if (currentContent != null)
        {
            pages = currentContent.pages;
            currentPage = pages[0];
        }

        for (int i = 0; i < pages.Count; i++)
        {
            for (int j = 0; j < pages[i].correctAnswers.Count; j++)
            {
                correctAsnwers.Add(pages[i].correctAnswers[j].answer);
            }
        }
    }

    private void Start()
    {
        SetupModel<DiaryController>();
       
    }

    public DiaryContent GetDiaryEntry()
    {
        return currentContent;
    }
}
