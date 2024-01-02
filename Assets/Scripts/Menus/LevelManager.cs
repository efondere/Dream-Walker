using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static uint LevelCount = 1;

    public static void CompleteLevel(uint levelNumber)
    {
        if (levelNumber >= GameData.GetLevel())
        {
            GameData.SetLevel(levelNumber + 1);
        }
    }

    public static uint GetCurrentLevel()
    {
        return GameData.GetLevel();
    }

    public static void LoadLevelScene(uint level)
    {
        SceneManager.LoadScene("Level" + level);
    }
}
