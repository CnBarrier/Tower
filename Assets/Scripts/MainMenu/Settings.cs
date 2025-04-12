using UnityEngine;

public class Settings : MonoBehaviour
{
    private CanvasGroup canvasgroup;

    void Start()
    {
        canvasgroup = GetComponent<CanvasGroup>();
        HideSettingsMenu();
    }

    public void HideSettingsMenu()
    {
        canvasgroup.alpha = 0;
        canvasgroup.interactable = false;
        canvasgroup.blocksRaycasts = false;
    }

    public void ShowSettingsMenu()
    {
        canvasgroup.alpha = 1;
        canvasgroup.interactable = true;
        canvasgroup.blocksRaycasts = true;
    }
}