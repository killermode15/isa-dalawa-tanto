using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : BaseView
{
    public enum ViewSide { Left, Right, Narration }

    public bool IsWritingDialogue => isWritingDialogue;

    [Header("Sprite UI References")]
    [SerializeField] private Image background;
    [SerializeField] private SpriteView leftSpriteView;
    [SerializeField] private SpriteView rightSpriteView;

    [Header("Dialogue Box References")]
    [SerializeField] private Image DialogueBoxUI;
    [SerializeField] private TextMeshProUGUI dialogueBox;
    
    [Space]
    [SerializeField] private Sprite leftDialogueSprite;
    [SerializeField] private Sprite rightDialogueSprite;
    [SerializeField] private Sprite narrationDialogueSprite;

    [Header("General References")]
    [SerializeField] private GameObject UI;

    [Header("Audio Reference")]
    [SerializeField] private AudioClip blipSound;

    private bool isWritingDialogue = false;

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
            dialogueBox.horizontalAlignment = HorizontalAlignmentOptions.Left;
            DialogueBoxUI.sprite = leftDialogueSprite;
        }
        else if (side == ViewSide.Right)
        {
            rightSpriteView.SetName(name);
            rightSpriteView.SetSprite(sprite, position);
            dialogueBox.horizontalAlignment = HorizontalAlignmentOptions.Right;
            DialogueBoxUI.sprite = rightDialogueSprite;
        }
        else
        {
            rightSpriteView.SetName("");
            rightSpriteView.SetSprite(null, position);
            dialogueBox.horizontalAlignment = HorizontalAlignmentOptions.Center;
            DialogueBoxUI.sprite = narrationDialogueSprite;
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
            dialogueBox.horizontalAlignment = HorizontalAlignmentOptions.Left;
            rightSpriteView.Clear();
            DialogueBoxUI.sprite = leftDialogueSprite;
        }
        else if (dialogue.Side == ViewSide.Right)
        {
            rightSpriteView.SetName(dialogue.Name.AsString());
            rightSpriteView.SetSprite(dialogue.CharacterSprite, dialogue.SpritePosition);
            dialogueBox.horizontalAlignment = HorizontalAlignmentOptions.Left;
            leftSpriteView.Clear();
            DialogueBoxUI.sprite = rightDialogueSprite;
        } else
        {
            rightSpriteView.SetName(string.Empty);
            rightSpriteView.SetSprite(null, dialogue.SpritePosition);
            dialogueBox.horizontalAlignment = HorizontalAlignmentOptions.Center;
            leftSpriteView.Clear();
            DialogueBoxUI.sprite = narrationDialogueSprite;
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
        isWritingDialogue = true;

        foreach(char letter in tempText)
        {
            dialogueBox.text += letter;
            AudioHelper.PlayClip(blipSound);
            yield return new WaitForSeconds(0.0275f);
        }

        dialogueBox.text = text;
        isWritingDialogue = false;
    }
}
