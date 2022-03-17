using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New Dialogue Database")]
public class DialogueDatabase : ScriptableObject
{
    [SerializeField] private List<DialogueBlob> dialogueBlobs;

    public DialogueBlob GetDialogueBlob(int id)
    {
        if (dialogueBlobs == null)
            return null;

        return dialogueBlobs.Find(x => x.Id == id);
    }
}
