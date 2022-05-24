using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Keep track on whether its paused or not
    // Used to prevent registering inputs on objects
    public static bool isPaused = false;

    // Pause menu UI panel
    public GameObject pauseMenuUI;

    void Update()
    {
        // Back button on Android is considered the Escape key
        // Only allow to pause if tutorial is done
        if (Input.GetKeyDown(KeyCode.Escape) && Tutorial.s_tutorialDone)
        {
            // Show or hide the pause menu UI
            isPaused = !isPaused;
            pauseMenuUI.SetActive(isPaused);
        }
    }

    public void OnResumePressed()
    {
        // No longer paused and hide pause menu
        isPaused = false;
        pauseMenuUI.SetActive(isPaused);
    }

    public void OnQuitPressed()
    {
        // No longer paused
        isPaused = false;
        pauseMenuUI.SetActive(isPaused);

        // Load main menu scene
        SceneManager.LoadScene(0);
    }
}
