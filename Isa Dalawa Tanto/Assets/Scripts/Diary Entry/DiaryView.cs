using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryView : BaseView
{
    [SerializeField] private Image paragraphContainer;
    [SerializeField] private Image doodleContainer;
    [SerializeField] private Transform answersParent;
    [SerializeField] private Transform livesParent;
    [SerializeField] private GameObject nextPageBtn;
    [SerializeField] private GameObject previousPageBtn;
    [SerializeField] private TMP_Text incorrectAnswersTxt;
    [SerializeField] private GameObject finishBtn;
    [SerializeField] private List<AnswerFieldHandler> createdAnswerFields;
    private List<GameObject> lives = new List<GameObject>();

    public List<AnswerFieldHandler> GetAnswerFieldHandlers()
    {
        return createdAnswerFields;
    }
    private void Start()
    {
        SetupView<DiaryController>();
    }

    public void SetupEntry(DiaryContent _entry, List<Page> _pages, int _pageNumber)
    {
        //set current paragraph
        Page _currentPage = _pages[_pageNumber];
        paragraphContainer.sprite = _currentPage.currentPargraph;

        //setup lives
        for (int i = 0; i < _entry.lives; i++)
        {
            GameObject life = Instantiate(_entry.lifePrefab, livesParent);
            life.name = "Health Icon";
            lives.Add(life);
        }
        Debug.Log("setup lives");

        //setup answer fields
        if (_currentPage.inputFieldPositions.Count > 0)
        {
            for (int i = 0; i < 1; i++)
            {
                CreateAnswerField(_entry, _currentPage, i);
            }
        }
        Debug.Log("setup answer fields");

        //toggle page buttons
        ToggleActiveUI(_entry, _pageNumber);
    }

    public bool ValidateEntries(List<Answer> _answers, List<Answer> _correctAnswers)
    {
        List<int> wrongNumbers = new List<int>();

        List<string> playerAnswers = new List<string>();
        List<string> correctAnswers = new List<string>();

        for (int i = 0; i < playerAnswers.Count; i++)
        {
            playerAnswers.Add(_answers[i].answer);
        }

        for(int i = 0; i<correctAnswers.Count; i++)
        {
            correctAnswers.Add(_correctAnswers[i].answer);
        }

        incorrectAnswersTxt.text = "The following answers are incorrect: \n";

        for (int i = 0; i < _answers.Count; i++)
        {
            if (playerAnswers[i] != correctAnswers[i])
            {
                wrongNumbers.Add(i + 1);
                return false;
            }
        }

        return true;
    }

    public void SetPage(DiaryContent _entry, Page _currentPage)
    {
        //remove previous answer fields
        createdAnswerFields.ForEach(x => Destroy(x.gameObject));
        createdAnswerFields.Clear();

        ToggleActiveUI(_entry, _entry.pages.IndexOf(_currentPage));
        SetParagraph(_currentPage.currentPargraph);
        SetDoodle(_currentPage.currentDoodle);

        //set new answerfields
        if (_currentPage.inputFieldPositions.Count > 0)
        {
            for (int i = 0; i < _currentPage.inputFieldPositions.Count; i++)
            {
                CreateAnswerField(_entry, _currentPage, i);
            }
        }
    }

    public void SetDoodle(Sprite _doodle)
    {
        doodleContainer.sprite = _doodle;
    }

    public void SetParagraph(Sprite _paragraph)
    {
        paragraphContainer.sprite = _paragraph;
    }

    public void SetExistingAnswers(List<Answer> _storedAnswers)
    {
        if (createdAnswerFields.Count == 0) return;

        for (int i = 0; i < _storedAnswers.Count; i++)
        {
            for (int j = 0; j < createdAnswerFields.Count; j++)
            {
                if(createdAnswerFields[j].Page == _storedAnswers[i].page && createdAnswerFields[j].ID == _storedAnswers[i].id)
                    createdAnswerFields[j].answerField.text = _storedAnswers[i].answer;
            }
        }
    }

    public void ToggleActiveUI(DiaryContent _entry, int _pageNumber)
    {
        if (_pageNumber == 0)
        {
            previousPageBtn.SetActive(false);
            nextPageBtn.SetActive(true);
            // incorrectAnswersTxt.gameObject.SetActive(false);
            finishBtn.SetActive(false);
            Debug.Log("First page");
        }

        if (_pageNumber == _entry.pages.Count - 1)
        {
            previousPageBtn.SetActive(true);
            nextPageBtn.SetActive(false);
            // incorrectAnswersTxt.gameObject.SetActive(true);
            finishBtn.SetActive(true);
            Debug.Log("Last page");
        }

        else if (_pageNumber > 0 && _pageNumber < _entry.pages.Count)
        {
            previousPageBtn.SetActive(true);
            nextPageBtn.SetActive(true);
            // incorrectAnswersTxt.gameObject.SetActive(false);
            finishBtn.SetActive(false);
            Debug.Log("Both");
        }
        Debug.Log(_pageNumber);
    }

    public void CreateAnswerField(DiaryContent _entry, Page _currentPage, int index)
    {
        GameObject answer = Instantiate(_entry.inputFieldPrefab, answersParent);
        answer.GetComponent<RectTransform>().anchoredPosition = new Vector3(_currentPage.inputFieldPositions[index].x, _currentPage.inputFieldPositions[index].y, 0);
        answer.name = "Answer Field" + (index + 1).ToString();
        answer.GetComponent<AnswerFieldHandler>().ID = index;
        answer.GetComponent<AnswerFieldHandler>().Page = _currentPage;

        createdAnswerFields.Add(answer.GetComponent<AnswerFieldHandler>());
    }

    public void ReduceHealth(int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            Destroy(lives[i]);
            lives.Remove(lives[i]);
        }
    }

    public int GetLives()
    {
        return lives.Count;
    }
}
