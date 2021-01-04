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

    private static Pooler _instance;

    private void Awake()
    {
        #region Singleton
        if (_instance == null)
        {
            _instance = this;
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
        foreach (var item in _itemsToPool)
        {
            for (var i = 0; i < item.amountToPool; i++)
            {
                var obj = Instantiate(item.objectToPool, _container, true);
                obj.name = item.objectToPool.name;
                obj.SetActive(false);
                _pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string objectName, Vector3 position, Quaternion rotation)
    {
        for (var i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy && _pooledObjects[i].name == objectName)
            {
                _pooledObjects[i].transform.position = position;
                _pooledObjects[i].transform.rotation = rotation;
                _pooledObjects[i].SetActive(true);
                return _pooledObjects[i];
            }
        }
        foreach (GameObjectToPool item in _itemsToPool)
        {
            if (item.objectToPool.name == objectName)
            {
                var pooledObject = Instantiate(item.objectToPool, _container, true);
                pooledObject.name = item.objectToPool.name;
                pooledObject.transform.position = position;
                pooledObject.transform.rotation = rotation;
                pooledObject.SetActive(true);
                _pooledObjects.Add(pooledObject);
                return pooledObject;
            }
        }
        return null;
    }
}