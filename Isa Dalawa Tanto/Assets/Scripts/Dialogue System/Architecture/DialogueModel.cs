using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueModel : BaseModel
{
    [SerializeField] private DialogueDatabase dialogueDatabase;

    // Start is called before the first frame update
    void Start()
    {
        SetupModel<DialogueController>();
    }

    public DialogueBlob GetDialogue(int id)
    {
        return dialogueDatabase.GetDialogueBlob(id);
    }
}
