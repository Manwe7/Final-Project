using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Barrel")]
    [SerializeField] private Transform barrel = null;
    [SerializeField] private GameObject Parent = null;
    [SerializeField] private GameObject bullet = null;

    [Header("Min and Max realod time")]
    [SerializeField] private int minReloadTime;
    [SerializeField] private int maxReloadTime;

    private float _offset = 270;
    private GameObject _player;
    private int _reloadTime;
    private bool _reloaded;
    private Pooler pooler;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        pooler = Pooler.Instance;
    }

    private void OnEnable()
    {
        StartCoroutine(Reload());
    }

    private void Update()
    {
        if(_player != null)
        {
            Shoote();

            Vector3 PlayerPos = _player.transform.position;

            //Change position depending in targets position
            if(PlayerPos.x < transform.position.x) 
            {
                LeftSide();
            } 
            else
            {
                RightSide();
            }
        }
    }

    private void Shoote()
    {
        if (_reloaded)
        {
            pooler.GetPooledObject(bullet.name, barrel.position, barrel.rotation);

            if (gameObject.activeSelf)
            {
                StartCoroutine(Reload());
            }
        }
    }

    protected IEnumerator Reload()
    {
        _reloaded = false;
        _reloadTime = Random.Range(minReloadTime, maxReloadTime);
        yield return new WaitForSeconds(_reloadTime);
        _reloaded = true;
    }

    private void RightSide()
    {
        //Rotate and change position to right
        Vector3 dir = _player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0f, rotZ + _offset);  

        transform.position = new Vector3(Parent.transform.position.x + 0.3f, Parent.transform.position.y, Parent.transform.position.z);
    }

    private void LeftSide()
    {
        //Rotate and change position to left
        Vector3 dir = _player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(-180f, 0f, -rotZ + _offset);  

        transform.position = new Vector3(Parent.transform.position.x - 0.3f, Parent.transform.position.y, Parent.transform.position.z);
    }
}
