using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [System.Serializable]
    public class GameObjectToPool
    {
        public GameObject objectToPool;
        public int amountToPool;
    }


    [SerializeField] private Transform _container = null;    
    
    public List<GameObjectToPool> _itemsToPool;
    private List<GameObject> _pooledObjects;

    public static Pooler Instance;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    private void Start()
    {       
        _pooledObjects = new List<GameObject>();
        foreach (GameObjectToPool item in _itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.transform.parent = _container;
                obj.name = item.objectToPool.name;
                obj.SetActive(false);
                _pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string name, Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy && _pooledObjects[i].name == name)
            {
                _pooledObjects[i].transform.position = position;
                _pooledObjects[i].transform.rotation = rotation;
                _pooledObjects[i].SetActive(true);
                return _pooledObjects[i];
            }
        }
        foreach (GameObjectToPool item in _itemsToPool)
        {
            if (item.objectToPool.name == name)
            {
                GameObject gameObject = Instantiate(item.objectToPool);
                gameObject.transform.parent = _container;
                gameObject.name = item.objectToPool.name;
                gameObject.transform.position = position;
                gameObject.transform.rotation = rotation;
                gameObject.SetActive(true);
                _pooledObjects.Add(gameObject);
                return gameObject;
            }
        }
        return null;
    }
}