using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryController : BaseController
{
    public static DiaryController Instance => instance;
    private static DiaryController instance;

    private void Start()
    {
        instance = this;
        SetupController<DiaryView, DiaryModel>();
    }

    private void Update()
    {
        
    }

    public IEnumerator SetupEntry()
    {
        yield return null;
    }
}
