using System.Collections.Generic;
using UnityEngine;

public class ExplosionPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }

    [SerializeField]
    private GameObject ExplosionContainer = null;

    public List<Pool> explosionPools;
    public Dictionary<string, Queue<GameObject>> explosionPoolDictionary;

    public static ExplosionPooler _instance;

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
        explosionPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in explosionPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.parent = ExplosionContainer.transform;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            explosionPoolDictionary.Add(pool.name, objectPool);
        }
    }

    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation)
    {
        if (!explosionPoolDictionary.ContainsKey(name))
        {
            Debug.Log("This name " + name + " does not excist");
            return null;
        }

        GameObject objectToSpawn = explosionPoolDictionary[name].Dequeue();
        if (objectToSpawn.activeSelf)
        {
            Debug.Log(objectToSpawn);
            InstantiatePoolObjects();
            objectToSpawn = explosionPoolDictionary[name].Dequeue();
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        explosionPoolDictionary[name].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
