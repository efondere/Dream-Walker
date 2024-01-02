using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenuManager : MonoBehaviour
{
    public Button[] levelButtons = { };

    // Start is called before the first frame update
    void Start()
    {
        UpdateMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadLevel(int levelID)
    {
        SceneManager.LoadScene("Level" + levelID);
    }

    public void UpdateMenu()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = i < LevelManager.GetCurrentLevel();
            //levelButtons[i].enabled = i < LevelManager.CurrentLevel;
        }
    }
}
