using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwap : MonoBehaviour {

    public Camera FstCamera;
    public Camera SndCamera;
    public Camera TrdCamera;
    public int CurrCam = 0;

    public void SwitchCam()
    {
        if (CurrCam == 0)
        {
            FstCamera.enabled = false;
            SndCamera.enabled = true;
            TrdCamera.enabled = false;
            CurrCam = 1;
        }
        else if (CurrCam == 1)
        {
            FstCamera.enabled = false;
            SndCamera.enabled = false;
            TrdCamera.enabled = true;
            CurrCam = 2;
        }
        else
        {
            FstCamera.enabled = true;
            SndCamera.enabled = false;
            TrdCamera.enabled = false;
            CurrCam = 0;
        }
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
