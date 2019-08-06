
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraChangerEditor : Editor
{
    [MenuItem("CONTEXT/CameraChanger/Apply Viewpoint")]
    static void ApplyViewpoint(MenuCommand command)
    {
        CameraChanger cc = (CameraChanger)command.context;

        var objectsToSave = new List<Object>();
        foreach (var v in cc.viewpoints)
        {
            objectsToSave.AddRange(v.objects);
        }
        var oj = objectsToSave.ToArray();
        Undo.RecordObjects(oj, "Apply Viewpoint");

        cc.ApplyActive();
    }
}
