using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    public uint levelNumber;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.CompleteLevel(levelNumber);

            if (levelNumber < LevelManager.LevelCount)
            {
                LevelManager.LoadLevelScene(levelNumber + 1);
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
