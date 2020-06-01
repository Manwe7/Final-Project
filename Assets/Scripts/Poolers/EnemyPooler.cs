using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }

    [SerializeField]
    private GameObject EnemyContainer = null;

    public List<Pool> enemyPools;
    public Dictionary<string, Queue<GameObject>> enemyPoolDictionary;

    public static EnemyPooler _instance;

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
        enemyPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in enemyPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.parent = EnemyContainer.transform;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            enemyPoolDictionary.Add(pool.name, objectPool);
        }
    }

    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation)
    {
        if (!enemyPoolDictionary.ContainsKey(name))
        {
            Debug.Log("This name " + name + " does not excist");
            return null;
        }

        GameObject objectToSpawn = enemyPoolDictionary[name].Dequeue();
        if (objectToSpawn.activeSelf)
        {
            Debug.Log(objectToSpawn);
            InstantiatePoolObjects();
            objectToSpawn = enemyPoolDictionary[name].Dequeue();
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        enemyPoolDictionary[name].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
