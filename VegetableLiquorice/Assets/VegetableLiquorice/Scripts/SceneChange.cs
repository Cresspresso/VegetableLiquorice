using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChange : MonoBehaviour {

    public string SceneName = "VegetableLiquoriceScene";
    float currentTime = 0;
    float TimeLimit = 10;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        currentTime = currentTime + Time.deltaTime;
		if (currentTime >= TimeLimit)
        {
            SceneManager.LoadScene(SceneName);
        }
	}
}
