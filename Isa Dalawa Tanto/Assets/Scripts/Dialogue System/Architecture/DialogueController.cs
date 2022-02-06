using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : BaseController
{
    public static DialogueController Instance => instance;

    private static DialogueController instance;

    private bool isDialogueOngoing = false;
    private bool shouldStartDialogue = false;

    private void Start()
    {
        instance = this;
        SetupController<DialogueView, DialogueModel>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            TriggerDialogue(0);

        NextDialogue();
    }

    public void TriggerDialogue(int id)
    {
        if (isDialogueOngoing)
            return;

        DialogueModel dm = model as DialogueModel;

        DialogueBlob dialogueBlob = dm.GetDialogue(id);
        isDialogueOngoing = true;
        StartCoroutine(StartDialogue(dialogueBlob));
    }

    private void NextDialogue()
    {
        if (isDialogueOngoing && !shouldStartDialogue)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shouldStartDialogue = true;
            }
        }
    }

    private IEnumerator StartDialogue(DialogueBlob dialogueBlob)
    {
        DialogueView dv = view as DialogueView;

        int index = 0;
        shouldStartDialogue = true;
        dv.ToggleUI(true);

        while (index < dialogueBlob.DialogueList.Count)
        {
            if (shouldStartDialogue)
            {
                Dialogue dialogue = dialogueBlob.DialogueList[index];
                dv.SetDialogueUI(dialogue);

                shouldStartDialogue = false;
                index++;
            }
            yield return new WaitForEndOfFrame();
        }

        isDialogueOngoing = false;
        dv.ToggleUI(false);
        yield return null;
    }
}
