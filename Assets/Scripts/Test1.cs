using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test1 : MonoBehaviour
{
    public delegate void Explosion();

    public static event Explosion Exploded;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Exploded != null)
            { Exploded(); }
        }
    }
}
