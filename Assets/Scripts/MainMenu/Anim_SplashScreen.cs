using System.Collections;
using UnityEngine;

public class Anim_Title : MonoBehaviour
{
    public Animator anim;
    public CanvasGroup canvasGroup;

    void Start()
    {
        Title();
    }

    public void Title()
    {
        anim.Play("Title");
        Invoke(nameof(FadeOutSplashScreen), 3f);
    }
    
    public void FadeOutSplashScreen()
    {
        StartCoroutine(FadeCoroutine(1.5f)); 
    }

    IEnumerator FadeCoroutine(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }
}
