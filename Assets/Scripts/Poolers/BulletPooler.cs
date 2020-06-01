using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }

    [SerializeField]
    private GameObject BulletContainer = null;

    public List<Pool> bulletPools;
    public Dictionary<string, Queue<GameObject>> bulletPoolDictionary;

    public static BulletPooler _instance;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        InstantiatePoolObjects();
    }

    private void InstantiatePoolObjects()
    {
        bulletPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in bulletPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.parent = BulletContainer.transform;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            bulletPoolDictionary.Add(pool.name, objectPool);
        }
    }

    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation)
    {
        if (!bulletPoolDictionary.ContainsKey(name))
        {
            Debug.Log("This name " + name + " does not excist");
            return null;
        }

        GameObject objectToSpawn = bulletPoolDictionary[name].Dequeue();
        if (objectToSpawn.activeSelf)
        {
            Debug.Log(objectToSpawn);
            InstantiatePoolObjects();
            objectToSpawn = bulletPoolDictionary[name].Dequeue();
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        bulletPoolDictionary[name].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
