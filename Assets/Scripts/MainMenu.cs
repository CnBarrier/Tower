using UnityEngine;
public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

    public void Settings()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!");
    }

}
