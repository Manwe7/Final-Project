using UnityEngine;

public abstract class BaseIntRepo : IRepo<int>
{
    protected abstract string _key { get; }
    
    public void Save(int value)
    {
        PlayerPrefs.SetInt(_key, value);
    }

    public int Get()
    {
        return PlayerPrefs.GetInt(_key, 0);
    }
}