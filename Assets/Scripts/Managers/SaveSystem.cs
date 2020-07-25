using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string record = "Record";

    private int _record;

    public void SaveRecord(int value)
    {
        PlayerPrefs.SetInt(record, value);
    }

    public int GetRecord()
    {
        _record = PlayerPrefs.GetInt(record, 0);
        return _record;
    }
}
