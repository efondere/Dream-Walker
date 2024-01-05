using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private List<GameObject> _gameObjects = new List<GameObject>();

    public static GameObjectPool CreatePool(GameObject prefab)
    {
        GameObject go = new GameObject(prefab.name);
        GameObjectPool pool = go.AddComponent<GameObjectPool>();
        pool._prefab = prefab;

        return pool;
    }

    public GameObject Instantiate(Vector3 position, Quaternion rotation)
    {
        foreach (var gameObject in _gameObjects)
        {
            if (gameObject.activeInHierarchy)
            {
                continue;
            }
            else
            {
                gameObject.transform.position = position;
                gameObject.transform.rotation = rotation;
                gameObject.SetActive(true);

                return gameObject;
            }
        }

        // not enough game objects.
        var obj = Instantiate(_prefab, transform); // create as child of GameObjectPool
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        _gameObjects.Add(obj);
        return _gameObjects[_gameObjects.Count - 1];
    }

    public void ClearAll()
    {
        foreach (var gameObject in _gameObjects)
        {
            gameObject.SetActive(false);
        }
    }

    public void DestroyAll()
    {
        // TODO: needs testing (will it destroy destroyed objects?)
        foreach (var gameObject in _gameObjects)
        {
            Destroy(gameObject);
        }

        _gameObjects.Clear();
    }
}
