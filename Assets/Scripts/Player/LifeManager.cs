using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int Lives;
    public GameObject[] healthUI;
    private float startDeathTime = 0.5f;
    private float deathTime;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        deathTime = startDeathTime;
        foreach (GameObject health in healthUI)
        {
            health.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Health ui + death
        for (int i = 0; i < healthUI.Length; i++)
        {
            healthUI[i].SetActive(i < Lives);
        }

        if (Lives <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        if (deathTime <= 0f)
        {
            animator.SetBool("IsDead", false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            if (!animator.GetBool("IsDead"))
            {
                animator.SetBool("IsDead", true);
                GetComponent<AudioSource>().Play();
            }
            deathTime -= Time.deltaTime;

        }
    }
}
