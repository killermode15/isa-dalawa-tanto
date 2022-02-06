using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCameraConstraint : MonoBehaviour
{
    [SerializeField] private Vector2 constraint = new Vector2(-1, 1);
    public Vector2 Constraint
    {
        get => constraint;
        set => constraint = value;
    }

    public void SetConstraint()
    {
        CameraHandler.Instance.HorizontalClamp = constraint;
    }
}
