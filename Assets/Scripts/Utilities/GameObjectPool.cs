using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private List<GameObject> _gameObjects = new List<GameObject>();

    public static void CreatePool(ref GameObjectPool pool ,GameObject prefab)
    {
        GameObject go = new GameObject(prefab.name);
        pool = go.AddComponent<GameObjectPool>();
        pool._prefab = prefab;
    }

    public void Instantiate(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            if (_gameObjects[i].activeInHierarchy)
            {
                continue;
            }
            else
            {
                _gameObjects[i].transform.position = position;
                _gameObjects[i].transform.rotation = rotation;
                _gameObjects[i].SetActive(true);

                return;
            }
        }
        
        // not enough game objects.
        var obj = Instantiate(_prefab, transform); // create as child of GameObjectPool
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        
        _gameObjects.Add(obj);
    }

    public void ClearAll()
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            _gameObjects[i].SetActive(false);
        }
    }
}
