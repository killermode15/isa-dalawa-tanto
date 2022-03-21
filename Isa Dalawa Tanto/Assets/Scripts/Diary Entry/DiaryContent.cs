using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Diary Entry/New Diary Entry")]
public class DiaryContent : ScriptableObject
{
    public int Id;
    public int lives;
    public List<Page> pages;

    [Header("Prefabs")]
    public GameObject lifePrefab;
    public GameObject inputFieldPrefab;
}

[System.Serializable]
public class Page
{
    public Sprite currentPargraph;
    public Sprite currentDoodle; 
    public List<Vector2> inputFieldPositions;
    public List<Answer> correctAnswers;
}

[System.Serializable]
public class Answer
{
    public int  id;
    public Page page;
    public string answer;

    public Answer(int _id, Page _page, string _answer)
    {
        id = _id;
        page = _page;
        answer = _answer;
    }
}
