using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RetryGame()
    {
        Time.timeScale = 1f; // Ensure game time resumes
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
