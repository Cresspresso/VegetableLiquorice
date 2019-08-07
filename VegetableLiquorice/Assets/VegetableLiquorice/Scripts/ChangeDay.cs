using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeDay : MonoBehaviour {

    public Animator animator;

    private int levelToLoad;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
		/*if (Input.GetMouseButtonDown(0))
        {
            FadeToLevel(0);
        }*/
	}

    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
