using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    public int LevelNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.CompleteLevel(LevelNumber);

            if (LevelNumber < LevelManager.LevelCount)
            {
                int nextLevel = LevelNumber + 1;
                SceneManager.LoadScene("Level" + nextLevel);
            }
            else
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
    }
}
