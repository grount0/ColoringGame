using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class UDictionaryExample : MonoBehaviour
{


    //[UDictionary.Split(100, 100)]
    public UDictionary1 dictionary1;
    [Serializable]
    public class UDictionary1 : UDictionary<ButtonObject, Colors> { }

    [UDictionary.Split(50, 50)]
    public UDictionary2 dictionary2;
    [Serializable]
    public class UDictionary2 : UDictionary<Key, Value> { }




   
    

    [Serializable]
    public class Key
    {
        public ImageScript WhiteImage;


    }

    [Serializable]
    public class Value
    {
        

        public Sprite ColorfulImage;
    }
    [Serializable]
    public class ButtonObject
    {
        public GameObject Object;


    }

    [Serializable]
    public class Colors
    {


        public Color32 ForegroundColor;
        public Color32 BackgroundColor;
    }

    void Start()
    {
        
    }
}