using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    private Vector2 _pointA, _pointB;

    private void Start()
    {
        _pointA = new Vector2(transform.position.x, transform.position.y + 4f);
        _pointB = new Vector2(transform.position.x, transform.position.y - 4f);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(_pointA, _pointB, Mathf.PingPong(Time.time / 2, 1));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
