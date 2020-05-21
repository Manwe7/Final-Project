using System.Security.Cryptography;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private void Start()
    {
        Invoke("SelfDestroy", 5f);       
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
