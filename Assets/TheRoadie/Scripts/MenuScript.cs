using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Animator animator;

    public string cutsceneScene = "Cutscene1";
    public string exitScene = "exit";
    public string sceneToLoad;

    public void playCutscene()
    {
        sceneToLoad = cutsceneScene;
        animator.SetTrigger("FadeOut");
    }

    public void exitGame()
    {
        sceneToLoad = exitScene;
        animator.SetTrigger("FadeOut");
    }

    public void onFadeComplete()
    {
        if (sceneToLoad == exitScene)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
