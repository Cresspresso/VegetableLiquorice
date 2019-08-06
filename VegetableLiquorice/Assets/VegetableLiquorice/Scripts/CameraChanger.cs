
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [Serializable]
    public class Viewpoint
    {
        public GameObject[] objects = new GameObject[0];

        public void SetActive(bool v)
        {
            foreach (var go in objects)
            {
                go.SetActive(v);
            }
        }
    }

    public Viewpoint[] viewpoints = new Viewpoint[1] { new Viewpoint() };
    public int current = 0;

    public void ChangeToIndex(int index)
    {
        current = index;
        ApplyActive();
    }

    public void ApplyActive()
    {
        for (int i = 0; i < viewpoints.Length; i++)
        {
            var v = viewpoints[i];
            if (v == null) { continue; }
            v.SetActive(i == current);
        }
    }

    private void Start()
    {
        ApplyActive();
    }
}
