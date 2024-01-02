using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundsDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // TODO: we should maybe move this to another place so we control what happens when we
            // exit the level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
