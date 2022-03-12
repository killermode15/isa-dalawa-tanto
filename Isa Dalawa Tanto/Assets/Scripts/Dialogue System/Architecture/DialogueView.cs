using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : BaseView
{
    public enum ViewSide { Left, Right}
    
    [SerializeField] private SpriteView leftSpriteView;
    [SerializeField] private SpriteView rightSpriteView;
    [SerializeField] private Image DialogueBoxUI;
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private Image background;

    [SerializeField] private Sprite leftDialogueSprite;
    [SerializeField] private Sprite rightDialogueSprite;
    [SerializeField] private GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(false);
        SetupView<DialogueController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDialogueUI(ViewSide side, Sprite sprite, SpriteView.Position position, string name)
    {
        if (side == ViewSide.Left)
        {
            leftSpriteView.SetName(name);
            leftSpriteView.SetSprite(sprite, position);
            DialogueBoxUI.sprite = leftDialogueSprite;
        }
        else
        {
            rightSpriteView.SetName(name);
            rightSpriteView.SetSprite(sprite, position);
            DialogueBoxUI.sprite = rightDialogueSprite;
        }
    }

    public void SetDialogueBackground(Sprite backgroundSprite = null)
    {
        if(!backgroundSprite)
        {
            background.color = Color.clear;
        }
        else
        {
            background.color = Color.white;
            background.sprite = backgroundSprite;
        }
    }

    public void SetDialogueUI(Dialogue dialogue)
    {
        if (dialogue.Side == ViewSide.Left)
        {
            leftSpriteView.SetName(dialogue.Name.AsString());
            leftSpriteView.SetSprite(dialogue.CharacterSprite, dialogue.SpritePosition);
            rightSpriteView.Clear();
            DialogueBoxUI.sprite = leftDialogueSprite;
        }
        else
        {
            rightSpriteView.SetName(dialogue.Name.AsString());
            rightSpriteView.SetSprite(dialogue.CharacterSprite, dialogue.SpritePosition);
            leftSpriteView.Clear();
            DialogueBoxUI.sprite = rightDialogueSprite;
        }

        StopAllCoroutines();
        StartCoroutine(SetDialogueText(dialogue.Text));
    }

    public void ToggleUI(bool state)
    {
        UI.SetActive(state);
    }

    private IEnumerator SetDialogueText(string text)
    {
        dialogueBox.text = string.Empty;

        string tempText = text.RemoveRichTextTag();

        foreach(char letter in tempText)
        {
            dialogueBox.text += letter;
            yield return new WaitForEndOfFrame();
        }

        dialogueBox.text = text;
    }
}
