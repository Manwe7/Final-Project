using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform Container = null;

    public static ObjectPool Instance;
    //public List<ObjectPoolItem> itemsToPool;
    public ObjectPoolItem[] _poolItems = new ObjectPoolItem[0];
    //private List<GameObject> pooledObjects;

    Dictionary<ObjectPoolItem.ObjectType, List<GameObject>> _pool;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        _pool = new Dictionary<ObjectPoolItem.ObjectType, List<GameObject>>();
        foreach (ObjectPoolItem item in _poolItems)
        {
            if (!_pool.ContainsKey(item.objectType))
            {
                _pool.Add(item.objectType, new List<GameObject>());
            }

            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject gameObject = Instantiate(item.objectToPool);
                PutObjectToPool(item.objectType, gameObject);
            }
        }
    }

    public void PutObjectToPool(ObjectPoolItem.ObjectType _objectType, GameObject _gameObject)
    {
        if (!_pool.ContainsKey(_objectType))
        {
            return;
        }
        gameObject.transform.parent = Container;
        gameObject.SetActive(false);
        _pool[_objectType].Add(gameObject);
    }

    public GameObject GetGameObjectFromPool(
                                        ObjectPoolItem.ObjectType objectType,
                                        Vector3 position,
                                        Quaternion rotation
                                        )
    {

        //not in the pool
        if (!_pool.ContainsKey(objectType))
        {
            Debug.Log("ERROR");
            return null;
        }

        GameObject gameObject;
        //pool is empty - add object
        if (_pool[objectType].Count == 0)
        {
            //if we have a key in dictionary - there must be a prefab defined
            var prefab = _poolItems.First(p => p.objectType == objectType).objectToPool;            
            gameObject = (GameObject)Instantiate(prefab.gameObject);
        }
        else
        {
            gameObject = _pool[objectType][0];
            _pool[objectType].RemoveAt(0);
        }

        gameObject.SetActive(true);
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;

        return gameObject;
    }
}
