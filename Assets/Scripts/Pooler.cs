using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private Transform Container = null;

    [System.Serializable]
    public class ObjectPoolItem
    {
        public string objectName;
        public GameObject objectToPool;
        public int amountToPool;
        public bool shouldExpand;
    }

    public static Pooler Instance;
    public List<ObjectPoolItem> itemsToPool;
    private List<GameObject> pooledObjects;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {       
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.parent = Container;
                obj.name = item.objectName;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string name)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].name == name)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.name == name)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.transform.parent = Container;
                    obj.name = item.objectName;
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}