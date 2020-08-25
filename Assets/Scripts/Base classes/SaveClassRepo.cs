using UnityEngine;
using System.IO;
using System.Text;
using System;

public class SaveClassRepo : IRepo<SaveAttributes>
{
    private readonly string _filePath = Path.Combine(Application.persistentDataPath, "save.json");

    private readonly string _backUpKey = "key";

    public void Save(SaveAttributes value)
    {
        string json = JsonUtility.ToJson(value);
        File.WriteAllText(_filePath, json);

        //Encode json
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(json);
        string encodedText = Convert.ToBase64String(bytesToEncode);
        
        //Save to playreprefs
        PlayerPrefs.SetString(_backUpKey, encodedText);
    }

    public SaveAttributes Get()
    {
        if(File.Exists(_filePath))
        {          
            string savedString = File.ReadAllText(_filePath);
            SaveAttributes myObject = JsonUtility.FromJson<SaveAttributes>(savedString);

            string backUp = PlayerPrefs.GetString(_backUpKey, "");
            
            //Decode json
            byte[] decodedBytes = Convert.FromBase64String(backUp);
            string decodedText = Encoding.UTF8.GetString(decodedBytes);

            if(savedString != decodedText)
            {
                savedString = backUp;
                SaveAttributes backUpObject = JsonUtility.FromJson<SaveAttributes>(savedString);

                return backUpObject;
            }

            return myObject;
        }
        else
        {
            return new SaveAttributes();
        }
    }

    //Saves JSON in assets
    // public void Save(SaveAttributes value)
    // {
    //     string json = JsonUtility.ToJson(value);
    //     File.WriteAllText(Application.dataPath + "/save.txt", json);

    //     //Encode json
    //     byte[] bytesToEncode = Encoding.UTF8.GetBytes(json);
    //     string encodedText = Convert.ToBase64String(bytesToEncode);
        
    //     //Save to playreprefs
    //     PlayerPrefs.SetString(_backUpKey, encodedText);
    // }

    // public SaveAttributes Get()
    // {
    //     if(File.Exists(Application.dataPath + "/save.txt"))
    //     {
    //         string savedString = File.ReadAllText(Application.dataPath + "/save.txt");
    //         SaveAttributes myObject = JsonUtility.FromJson<SaveAttributes>(savedString);


    //         string backUp = PlayerPrefs.GetString(_backUpKey, "");
            
    //         //Decode json
    //         byte[] decodedBytes = Convert.FromBase64String(backUp);
    //         string decodedText = Encoding.UTF8.GetString(decodedBytes);

    //         if(savedString != decodedText)
    //         {
    //             savedString = decodedText;
    //             SaveAttributes backUpObject = JsonUtility.FromJson<SaveAttributes>(savedString);
    //             Save(backUpObject);

    //             return backUpObject;
    //         }

    //         return myObject; //base 64 encode
    //     }
    //     else
    //     {
    //         return new SaveAttributes(); // create a new one
    //     }
    // }
}