using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [System.Serializable]
    public class GameObjectToPool
    {
        public string objectName;
        public GameObject objectToPool;
        public int amountToPool;
        public bool shouldExpand;
    }


    [SerializeField] private Transform Container = null;    

    public static Pooler Instance;
    public List<GameObjectToPool> itemsToPool;
    private List<GameObject> pooledObjects;

    void Awake()
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

    // Use this for initialization
    void Start()
    {       
        pooledObjects = new List<GameObject>();
        foreach (GameObjectToPool item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.transform.parent = Container;
                obj.name = item.objectName;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string name, Vector3 _position, Quaternion _rotation)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].name == name)
            {
                pooledObjects[i].transform.position = _position;
                pooledObjects[i].transform.rotation = _rotation;
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        foreach (GameObjectToPool item in itemsToPool)
        {
            if (item.objectToPool.name == name)
            {
                if (item.shouldExpand)
                {
                    GameObject gameObject = Instantiate(item.objectToPool);
                    gameObject.transform.parent = Container;
                    gameObject.name = item.objectName;
                    gameObject.transform.position = _position;
                    gameObject.transform.rotation = _rotation;
                    gameObject.SetActive(true);
                    pooledObjects.Add(gameObject);
                    //return obj;
                }
            }
        }
        return null;
    }
}