using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(Bridge))]
[CanEditMultipleObjects]
public class BridgeEditor : Editor {

    void OnSceneGUI()
    {
        Bridge targetBridge = (Bridge)target;

        Handles.color = Color.cyan;

        EditorGUI.BeginChangeCheck();

        Vector3 leftAnchor = Handles.PositionHandle(targetBridge.transform.position + targetBridge.leftAnchor, Quaternion.identity);
        Vector3 rightAnchor = Handles.PositionHandle(targetBridge.transform.position + targetBridge.rightAnchor, Quaternion.identity);
        

        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Anchor Position");
            targetBridge.leftAnchor = leftAnchor - targetBridge.transform.position;
            targetBridge.rightAnchor = rightAnchor - targetBridge.transform.position;
        }
        Vector3 minToLeft = new Vector3(Mathf.Min(leftAnchor.x, targetBridge.transform.position.x), Mathf.Min(leftAnchor.y, targetBridge.transform.position.y), Mathf.Min(leftAnchor.z, targetBridge.transform.position.z));
        Vector3 minToRight = new Vector3(Mathf.Max(rightAnchor.x, targetBridge.transform.position.x), Mathf.Min(rightAnchor.y, targetBridge.transform.position.y), Mathf.Min(rightAnchor.z, targetBridge.transform.position.z));

        Handles.DrawBezier(targetBridge.transform.position, leftAnchor, targetBridge.transform.position + (minToLeft - targetBridge.transform.position).normalized, leftAnchor + (minToLeft - leftAnchor).normalized, Color.green, null,HandleUtility.GetHandleSize(Vector3.zero));
        Handles.DrawBezier(targetBridge.transform.position, rightAnchor, targetBridge.transform.position + (minToRight - targetBridge.transform.position).normalized,rightAnchor + (minToRight- rightAnchor).normalized, Color.green, null, HandleUtility.GetHandleSize(Vector3.zero));

    }


}
