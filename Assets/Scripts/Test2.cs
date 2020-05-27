using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test2 : MonoBehaviour
{
    private void OnEnable()
    {
        Test1.Exploded += Explode;
    }

    private void OnDisable()
    {
        Test1.Exploded -= Explode;
    }

    private void Explode()
    {
        Debug.Log("Some stuff");
    }
}
