using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScalar : MonoBehaviour {

    public Vector3 maxScale = new Vector3(5f, 5f, 5f);
    public float ScaleFactor = 1.2f;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 currentScale = this.transform.localScale;
        currentScale = currentScale + Vector3.one * ScaleFactor * Time.deltaTime;
        if (currentScale.x > maxScale.x
            || currentScale.y > maxScale.y
            || currentScale.z > maxScale.z)
        {
            currentScale = maxScale;
        }
        this.transform.localScale = currentScale;
    }
}
