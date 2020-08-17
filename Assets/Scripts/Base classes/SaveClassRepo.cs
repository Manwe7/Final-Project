using UnityEngine;
using System.IO;

public class SaveClassRepo : IRepo<SaveAttributes>
{
    public void Save(SaveAttributes value)
    {
        string json = JsonUtility.ToJson(value);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public SaveAttributes Get()
    {
        if(File.Exists(Application.dataPath + "/save.txt"))
        {
            string savedString = File.ReadAllText(Application.dataPath + "/save.txt");
            SaveAttributes myObject = JsonUtility.FromJson<SaveAttributes>(savedString);        

            return myObject; //base 64 encode
        }
        else
        {
            return new SaveAttributes(); // create a new one
        }
    }

    // public void Save(int value)
    // {
    //     PlayerPrefs.SetInt(_key, value);
    // }

    // public int Get()
    // {
    //     return PlayerPrefs.GetInt(_key, 0);
    // }
}