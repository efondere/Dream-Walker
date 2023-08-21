using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    public uint levelNumber;

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
            LevelManager.CompleteLevel(levelNumber);

            if (levelNumber < LevelManager.LevelCount)
            {
                uint nextLevel = levelNumber + 1;
                SceneManager.LoadScene("Level" + nextLevel);
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
