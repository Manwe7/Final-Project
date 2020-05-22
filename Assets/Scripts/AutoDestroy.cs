using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private void Start()
    {
        Invoke("SelfDestroy", 5f);       
    }

    private void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
}
