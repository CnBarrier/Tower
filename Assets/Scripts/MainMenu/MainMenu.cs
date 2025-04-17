using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public Animator animator;
    
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void ShowMainMenu()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }    
 
    public void HideMainMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void Animations_Lishi()
    {
        
    }
}
