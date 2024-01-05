using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button _continueButton;

    public void Start()
    {
        _continueButton.interactable = GameData.GetLevel() > 0;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadCurrentLevel()
    {
        LevelManager.LoadLevelScene(LevelManager.GetCurrentLevel());
    }

    public void OpenLevelSelector()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void OpenCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
}
