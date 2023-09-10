using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlarmBehavior : MonoBehaviour
{
    public void Death()
    {
        SceneManager.LoadScene("Level0.5");
    }
}
