using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int CurrentLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void CompleteLevel(int levelNumber)
    {
        if (levelNumber >= CurrentLevel)
        {
            CurrentLevel = levelNumber + 1;
        }
    }
}
