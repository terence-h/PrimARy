using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // UIs on the main menu
    public GameObject[] mainMenuUI;
    public GameObject[] quitMenu;

    public void StartGame()
    {
        // Load the gameplay scene
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        // Back button on Android is considered the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If on the main menu UI
            if (mainMenuUI[0].activeSelf)
            {
                // Hide all main menu UI
                for (int i = 0; i < mainMenuUI.Length; i++)
                {
                    mainMenuUI[i].SetActive(false);
                }

                // Show all quit confirmation menu UI
                for (int i = 0; i < quitMenu.Length; i++)
                {
                    quitMenu[i].SetActive(true);
                }
            }
            // If on the quit confirmation menu UI
            else
            {
                // Hide all quit confirmation menu UI
                for (int i = 0; i < quitMenu.Length; i++)
                {
                    quitMenu[i].SetActive(false);
                }

                // Show all main menu UI
                for (int i = 0; i < mainMenuUI.Length; i++)
                {
                    mainMenuUI[i].SetActive(true);
                }
            }
        }
    }

    public void QuitMenu()
    {
        // Hide all main menu UI
        for (int i = 0; i < mainMenuUI.Length; i++)
        {
            mainMenuUI[i].SetActive(false);
        }

        // Show all quit confirmation menu UI
        for (int i = 0; i < quitMenu.Length; i++)
        {
            quitMenu[i].SetActive(true);
        }
    }

    public void ConfirmQuit()
    {
        // Quit Game
        Application.Quit();
    }

    public void CancelQuit()
    {
        // Hide all quit confirmation menu UI
        for (int i = 0; i < quitMenu.Length; i++)
        {
            quitMenu[i].SetActive(false);
        }

        // Show all main menu UI
        for (int i = 0; i < mainMenuUI.Length; i++)
        {
            mainMenuUI[i].SetActive(true);
        }
    }

    public void ResetTutorial()
    {
        PlayerPrefs.SetInt(Tutorial.s_tutorialSaveFile, 0);
    }
}