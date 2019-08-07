
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        foreach (var v in viewpoints.Where(v => v != null))
        {
            v.SetActive(false);
        }

        if (current >= 0 && current < viewpoints.Length)
        {
            viewpoints[current].SetActive(true);
        }
    }

    private void Start()
    {
        ApplyActive();
    }
}
