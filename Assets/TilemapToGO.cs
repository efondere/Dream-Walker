using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class TilemapToGO : MonoBehaviour
{
    public Tilemap tm;
    public GameObject goParent;

    int minX, minY, maxX, maxY;


    public void FindBounds()
    {
        minX = minY = int.MaxValue;

        for (int x = 0; x <= (int)tm.size.x; x++)
        {
            for (int y = 0; y <= (int)tm.size.y; y++)
            {
                var pos = new Vector3Int(x, y, 0);
                if (tm.GetSprite(pos) != null)
                {
                    if (pos.x < minX)
                    {
                        minX = pos.x;
                    }
                    if (pos.y < minY)
                    {
                        minY = pos.y;
                    }
                    if (pos.y > maxY)
                    {
                        maxY = pos.y;
                    }
                    if (pos.x > maxX)
                    {
                        maxX = pos.x;
                    }
                }
            }
        }
    }

    public void CreateGO()
    {
        GameObject go = new GameObject();
        SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                Vector3Int pos = new Vector3Int(x, y);
                if (tm.GetSprite(pos) != null)
                {
                    go.name = tm.GetSprite(pos).name;
                    spriteRenderer.sprite = tm.GetSprite(pos);
                    Instantiate(go, pos, Quaternion.identity, goParent.transform);
                }
            }
        }
        DestroyImmediate(go);


    }

}
