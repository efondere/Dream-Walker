using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using static UnityEditor.PlayerSettings;
using UnityEngine.U2D;
using System;
using System.Reflection;
using System.Linq;

public class TilemapToTexture : MonoBehaviour
{
    [HideInInspector] public Texture2D tx;
    public Texture2D unslicedTx;
    public Tilemap tm;
    public Sprite sampleSprite;
    int minX, minY, maxX, maxY;


    public void CreateTx()
    {
        var width = sampleSprite.rect.width;
        var height = sampleSprite.rect.height;

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

        Debug.Log("min vector "+ new Vector2Int(minX, minY) + "max vector " + new Vector2Int(maxX, maxY));

        var texture = new Texture2D((int)width * (maxX - minX + 1), (maxY - minY + 1) * (int)height);
        Color[] transparent = new Color[texture.width * texture.height];
        for (int i = 0; i < transparent.Length; i++)
        {
            transparent[i] = new Color(0, 0, 0, 0);
        }
        texture.SetPixels(0, 0, texture.width, texture.height, transparent);

        for (int x = minX; x <= maxX ; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                var pos = new Vector3Int(x, y, 0);
                if (tm.GetSprite(pos) != null)
                {
                    Debug.Log(pos == new Vector3Int(minX, minY));
                    //Debug.Log("new x pos " + x);
                    var sprite = tm.GetSprite(pos);
                    var pixels = unslicedTx.GetPixels((int)tm.GetSprite(pos).rect.x, (int)tm.GetSprite(pos).rect.y, (int)tm.GetSprite(pos).rect.width, (int)tm.GetSprite(pos).rect.height);
                    texture.SetPixels((x-minX) * (int)sprite.rect.width, (y-minY) * (int)sprite.rect.height, (int)sprite.rect.width, (int)sprite.rect.height, pixels);  
                }
  
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        tx = texture;
    }

    public void ExportAsPng(string name)
    {
        byte[] bytes = tx.EncodeToPNG();
        Debug.Log(tx.filterMode);
        var dirPath = Application.dataPath + "/Exported Tilemaps/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        File.WriteAllBytes(dirPath + name + ".png", bytes);

        AssetDatabase.Refresh();
        tx = null;
    }
}


public class TestClass
{

}

public class SubClass: TestClass { }

public class SubClass2 : TestClass { }

public class RetrieveObjects
{


    void Retrieve()
    {
    }

}

public static class GetSubclasses
{
    public static Type[] GetInheritedClasses(Type type)
    {
        return Assembly.GetAssembly(type).GetTypes().Where(theType => theType.IsClass && !theType.IsAbstract && theType.IsSubclassOf(type)).ToArray();
    }

}





 //   public delegate void myDelegate(string message);
 //   public void Test()
 //   {
 //
 //       myDelegate aDelegate = new myDelegate(DoSomething);
 //
 //       BeforeDoSomething(aDelegate);
 //
 //   }
 //
 //   public void DoSomething(string message)
 //   {
 //       Debug.Log(message);
 //   }
 //
 //   public void BeforeDoSomething(myDelegate function)
 //   {
 //       function("test");
 //   }