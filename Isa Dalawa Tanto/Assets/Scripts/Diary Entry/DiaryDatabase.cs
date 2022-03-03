using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryDatabase : ScriptableObject
{
    [SerializeField] private List<DiaryContent> diaryEntries;

    public DiaryContent GetDiaryEntry(int id)
    {
        if(id > diaryEntries.Count) return null;

        if(diaryEntries == null) return null;

        return diaryEntries.Find(x => x.Id == id);
    }
}
