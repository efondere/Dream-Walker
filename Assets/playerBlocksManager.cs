using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBlocksManager : MonoBehaviour
{
    public GameObject[] blocks;
    public int[] nbBlocksAvailable;

    [HideInInspector]public List<GameObject>[] blockList;
    private void Start()
    {
        CreatePool();
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
                Debug.Log(i);
            }
        }
    }
}
