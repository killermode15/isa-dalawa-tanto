using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SpriteView
{
    [Header("Sprite Positions")]
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private Image left;
    [SerializeField] private Image center;
    [SerializeField] private Image right;

    public enum Position
    {
        Left,
        Center,
        Right
    }

    public void SetName(string name)
    {
        this.name.text = name;
    }

    public void SetSprite(Sprite sprite, Position position, bool selectedPositionOnly = true)
    {
        switch (position)
        {
            case Position.Left:
                if (sprite == null)
                {
                    left.color = Color.clear;
                }
                else
                {
                    left.sprite = sprite;
                    left.color = Color.white;
                }
                if (selectedPositionOnly)
                {
                    center.sprite = null;
                    right.sprite = null;
                    center.color = Color.clear;
                    right.color = Color.clear;
                }
                break;
            case Position.Center:
                if (sprite == null)
                {
                    center.color = Color.clear;
                }
                else
                {
                    center.sprite = sprite;
                    center.color = Color.white;
                }
                if (selectedPositionOnly)
                {
                    left.sprite = null;
                    right.sprite = null;
                    left.color = Color.clear;
                    right.color = Color.clear;
                }
                break;
            case Position.Right:
                if (sprite == null)
                {
                    right.color = Color.clear;
                }
                else
                {
                    right.sprite = sprite;
                    right.color = Color.white;
                }
                if (selectedPositionOnly)
                {
                    center.sprite = null;
                    left.sprite = null;
                    center.color = Color.clear;
                    left.color = Color.clear;
                }
                break;
        }
    }

    public void Clear()
    {
        name.text = string.Empty;
        left.sprite = null;
        center.sprite = null;
        right.sprite = null;
        left.color = Color.clear;
        center.color = Color.clear;
        right.color = Color.clear;
    }
}