using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("StartGameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
