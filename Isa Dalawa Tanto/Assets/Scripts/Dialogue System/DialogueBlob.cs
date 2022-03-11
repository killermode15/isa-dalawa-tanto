using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New Dialogue Blob")]
public class DialogueBlob : ScriptableObject
{
    public int Id;
    public Sprite Background;
    public List<Dialogue> DialogueList;
}

[System.Serializable]
public class Dialogue
{
    public CharacterName Name;
    public Sprite CharacterSprite;
    public DialogueView.ViewSide Side;
    public SpriteView.Position SpritePosition;
    [TextArea]
    public string Text;
}

public enum CharacterName
{
    Null,
    Aspin,
    Tara,
    Cara,
    Gale,
    Lucky,
    MsAskal,
    MotherGoose,
    MrsCarabao,
    MrsMonkey,
    MrsTarsier,
    MrsEagle,
}
