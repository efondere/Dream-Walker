using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: Disable select behaviour
[RequireComponent(typeof(Pausable))]
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    public InputManager inputManager; // use this to error when no resume callback is given

    public void OnPause()
    {
        pauseMenuUI.SetActive(true);
    }

    public void OnResume()
    {
        pauseMenuUI.SetActive(false);
    }

    public void OnResumeClick()
    {
        inputManager.OnTriggerResume();
    }

    public void LoadMainMenu()
    {
        // TODO: do we need to resume? i.e. should we just resume everytime a scene is loaded anyways?
        PauseManager.Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        PauseManager.Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
