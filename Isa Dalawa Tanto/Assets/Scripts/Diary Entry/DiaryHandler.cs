using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaryHandler : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject lifePrefab;
    
    [SerializeField] private List<Image> paragraphs;
    [SerializeField] private List<string> correctAnswers;
    [SerializeField] private List<TMP_InputField> inputFields;
}
