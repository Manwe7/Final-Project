using System.Collections.Generic;
using UnityEngine;

public class BulletExplosionPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }

    [SerializeField]
    private GameObject BulletEContainer = null;

    public List<Pool> bulletEPools;
    public Dictionary<string, Queue<GameObject>> bulletEPoolDictionary;

    public static BulletExplosionPooler _instance;

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
        bulletEPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in bulletEPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.parent = BulletEContainer.transform;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            bulletEPoolDictionary.Add(pool.name, objectPool);
        }
    }

    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation)
    {
        if (!bulletEPoolDictionary.ContainsKey(name))
        {
            Debug.Log("This name " + name + " does not excist");
            return null;
        }

        GameObject objectToSpawn = bulletEPoolDictionary[name].Dequeue();
        if (objectToSpawn.activeSelf)
        {
            Debug.Log(objectToSpawn);
            InstantiatePoolObjects();
            objectToSpawn = bulletEPoolDictionary[name].Dequeue();
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        bulletEPoolDictionary[name].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
