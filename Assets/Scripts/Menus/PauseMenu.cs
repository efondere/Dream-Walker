using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Pausable
{
    public GameObject pauseMenuUI;

    public override void OnPause()
    {
        pauseMenuUI.SetActive(true);
    }

    public override void OnResume()
    {
        pauseMenuUI.SetActive(false);
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
