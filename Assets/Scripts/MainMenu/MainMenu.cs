using UnityEngine;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!");
    }
}
