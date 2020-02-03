using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class LoadingBar : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        this.rectTransform = this.GetComponent<RectTransform>();
    } 

    public void Set(float t) {
        this.rectTransform.localScale = new Vector3(t, 1, 1);
    }    
}

