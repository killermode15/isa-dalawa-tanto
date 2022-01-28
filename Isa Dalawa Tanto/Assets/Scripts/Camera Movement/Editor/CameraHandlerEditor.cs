using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraHandler))]
public class CameraHandlerEditor : Editor
{
    public void OnSceneGUI()
    {
        CameraHandler handler = target as CameraHandler;
        Transform transform = handler.transform;

        Vector3 leftClampPos = new Vector3(handler.HorizontalClamp.x, transform.position.y, transform.position.z);
        Vector3 rightClampPos = new Vector3(handler.HorizontalClamp.y, transform.position.y, transform.position.z);

        Handles.color = new Color(0.2f, 0.7f, 0.2f);
        Handles.DrawLine(leftClampPos, rightClampPos, 3);

        float leftSize = HandleUtility.GetHandleSize(leftClampPos) * 0.175f;
        float rightSize = HandleUtility.GetHandleSize(rightClampPos) * 0.175f;
        Vector3 snap = Vector3.one * 0.5f;

        EditorGUI.BeginChangeCheck();

        Handles.color = Color.red;
        Vector3 leftClampHandle = Handles.FreeMoveHandle(leftClampPos, transform.rotation, leftSize, snap, Handles.SphereHandleCap);

        Handles.color = Color.blue;
        Vector3 rightClampHandle = Handles.FreeMoveHandle(rightClampPos, transform.rotation, rightSize, snap, Handles.SphereHandleCap);


        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(handler, "Adjust clamp zones");
            handler.HorizontalClamp = new Vector2(leftClampHandle.x, rightClampHandle.x);

        }

        Repaint();
    }
}
