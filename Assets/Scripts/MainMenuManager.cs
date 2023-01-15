using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene("Level" + LevelManager.CurrentLevel);
    }

    public void OpenLevelSelector()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void OpenCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
}
