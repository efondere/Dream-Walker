using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBlocksManager : MonoBehaviour
{
    public GameObject[] tilesUI;
    public GameObject tileSelectorUI;

    [HideInInspector]public int m_currentlySelectedTile = -1;

    public GameObject[] blocks;
    public int[] nbBlocksAvailable;

    [HideInInspector]
    public bool isInDreamMode;

    [HideInInspector]public List<GameObject>[] blockList;
    private void Start()
    {
        CreatePool();

        m_currentlySelectedTile = tilesUI.Length / 2;

        setDreaming(false);
    }
    // Update is called once per frame

    void CreatePool()
    {
        blockList = new List<GameObject>[blocks.Length];
        for (int a = 0; a < blocks.Length; a++)
        {
            blockList[a] = new List<GameObject>();
            for (int i = 0; i < nbBlocksAvailable[a]; i++)
            {
                GameObject block = Instantiate(blocks[a], transform.position, Quaternion.identity);
                blockList[a].Add(block);
                block.SetActive(false);
            }
        }
    }

    public void setDreaming(bool isDreaming)
    {
        tileSelectorUI.SetActive(isDreaming);
        foreach (GameObject tileUI in tilesUI)
        {
            tileUI.SetActive(isDreaming);
        }

        isInDreamMode = isDreaming;
    }

    private void Update()
    {
        if (!isInDreamMode)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_currentlySelectedTile--;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            m_currentlySelectedTile++;
        }

        if (m_currentlySelectedTile >= tilesUI.Length)
        {
            m_currentlySelectedTile = 0;
        }
        else if (m_currentlySelectedTile < 0)
        {
            m_currentlySelectedTile = tilesUI.Length - 1;
        }

        tileSelectorUI.GetComponent<RectTransform>().position = tilesUI[m_currentlySelectedTile].GetComponent<RectTransform>().position;
    }
}
