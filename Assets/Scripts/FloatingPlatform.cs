using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    private Vector2 _pointA, _pointB;

    void Start()
    {
        _pointA = new Vector2(transform.position.x, transform.position.y + 4f);
        _pointB = new Vector2(transform.position.x, transform.position.y - 4f);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(_pointA, _pointB, Mathf.PingPong(Time.time / 2, 1));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
