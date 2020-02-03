using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneChangerScript : MonoBehaviour
{
    public Animator animator;

    public string prevScene;
    public string nextScene;
    public string sceneToLoad;
    
    public void playPrevScene()
    {
        sceneToLoad = prevScene;
        animator.SetTrigger("FadeOut");
    }

    public void playNextScene()
    {
        sceneToLoad = nextScene;
        animator.SetTrigger("FadeOut");
    }

    public void onFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
