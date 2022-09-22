using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public static class SaveGameManager 
{
    public static Save currentSave = new Save();

    public static string directory;
    public static string FileName;

    public static bool Save()
    {
        directory = currentSave.directory;
        FileName = currentSave.fileName;
        var dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
        {
            Debug.Log("yok");
            Directory.CreateDirectory(dir);
        }
        

        string json = JsonUtility.ToJson(currentSave, true);
        File.WriteAllText(dir+FileName, json);

        GUIUtility.systemCopyBuffer = dir;
        



        return true;
    }

    public static void LoadGame()
    {
        string fullPath = Application.persistentDataPath + directory + FileName;
        Debug.Log(fullPath);
        Save tempData = new Save();
        tempData.directory = currentSave.directory;
        tempData.fileName = currentSave.fileName;

        if(File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            tempData = JsonUtility.FromJson<Save>(json);
        }
        else
        {
            var dir = Application.persistentDataPath + directory;
            Directory.CreateDirectory(dir);
            Debug.LogError("Save File doesnt exists");
        }
        currentSave = tempData;
    }

   

    
    
}
