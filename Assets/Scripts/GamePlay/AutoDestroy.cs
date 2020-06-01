using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("SelfDestroy", 4f);       
    }

    private void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
}
