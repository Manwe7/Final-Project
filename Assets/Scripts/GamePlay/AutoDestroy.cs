using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(SelfDestroy());
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
