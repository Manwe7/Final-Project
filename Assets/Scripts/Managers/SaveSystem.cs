using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string record = "Record";

    public void SaveRecord(int value)
    {
        PlayerPrefs.SetInt("Record", value);
    }
}
