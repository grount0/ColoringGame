using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save 
{
    public List<int> objectIndex = new List<int>();
    public List<bool> SavedBoolList = new List<bool>();
    public int directoryId;
    public string directory;
    public string fileName;
   
}
