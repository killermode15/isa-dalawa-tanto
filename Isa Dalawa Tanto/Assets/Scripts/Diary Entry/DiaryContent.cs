using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Diary Entry/New Diary Entry")]
public class DiaryContent : ScriptableObject
{
    public int Id;
    public Entry entry;
}

[System.Serializable]
public class Entry
{
    [TextArea]
    public string paragraph;
    public Choice choice;
}

[System.Serializable]
public class Choice
{
    public List<string> choices;
    public List<string> correctChoices;
}