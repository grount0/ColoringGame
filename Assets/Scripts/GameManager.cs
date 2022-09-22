using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft;

public class GameManager : MonoBehaviour
{
    private Touch touch;
    private Vector3 touchPos;
    public int colorId;
    public List<ImageScript> imageList;
    public List<ButtonHealthScript> buttonList;
    public Material ditherMat, normalMat;

    public int canvasId;




    public List<ImageScript> colorfulImageList;



    public UDictionaryExample DictionaryInstance;

    public List<ImageScript> constantImageList;

    
    public delegate void PaintAction(ImageScript imageScr);
    public static  PaintAction onPaint;

    

    public delegate void ColorButtonTouchedAction(ButtonHealthScript buttonHealthScript);
    public static   ColorButtonTouchedAction onColorButtonTouched;


    public delegate void OnColorButtonHealthChanged(ButtonHealthScript buttonHealthScript);
    public static   OnColorButtonHealthChanged onColorButtonHealthChanged;




    private void OnEnable()
    {




        onColorButtonTouched += ColorButtonClicked;

        onPaint += RemoveImageFromList;
        onPaint += AddToColorfulList;
        onPaint += SetImageColor;
        onPaint += PaintObject;
        onPaint += SaveImageStatus;
        //onColorButtonHealthChanged += HandleButtonHealth;
        //onColorButtonHealthChanged += HandleButtonDiedAnim;

    }
    private void OnDisable()
    {
        //for (int i = 0; i < buttonList.Count; i++)
        //{

        //    buttonList[i].OnButtonDied -= RemoveFromButtonList;
        //}


        onColorButtonTouched -= ColorButtonClicked;

        onPaint -= RemoveImageFromList;
        onPaint -= AddToColorfulList;
        onPaint -= SetImageColor;
        onPaint -= PaintObject;
        onPaint -= SaveImageStatus;

        //onColorButtonHealthChanged -= HandleButtonHealth;
        //onColorButtonHealthChanged -= HandleButtonDiedAnim;

    }
    private void Awake()
    {

        SaveGameManager.currentSave.directoryId = canvasId;
        SaveGameManager.currentSave.directory = $"/SaveGame{SaveGameManager.currentSave.directoryId}/";


        SaveGameManager.currentSave.fileName = "SaveGame" + SaveGameManager.currentSave.directoryId + ".sav";
        SaveGameManager.directory = SaveGameManager.currentSave.directory;
        SaveGameManager.FileName = SaveGameManager.currentSave.fileName;
        foreach (var item in DictionaryInstance.dictionary2)
        {
            item.Key.WhiteImage.GetComponent<ImageScript>().colorfulImage = item.Value.ColorfulImage;
            imageList.Add(item.Key.WhiteImage.GetComponent<ImageScript>());
            constantImageList.Add(item.Key.WhiteImage.GetComponent<ImageScript>());
        }


        SaveGameManager.LoadGame();


        //buttonList.AddRange(GameObject.FindObjectsOfType<ButtonHealthScript>());

        foreach (var item in DictionaryInstance.dictionary1)
        {
            buttonList.Add(item.Key.Object.transform.GetChild(0).GetComponent<ButtonHealthScript>());



        }


        for (int i = 0; i < SaveGameManager.currentSave.objectIndex.Count; i++)
        {
            imageList[SaveGameManager.currentSave.objectIndex[i]].painted = true;

        }

        

    }



    public bool CheckIds(ImageScript image)
    {
        if (colorId == image.imageId)
            return true;
        else
            return false;

    }

    public void SetImageColor(ImageScript image)
    {
        if (!CheckIds(image) && !image.painted)
            return;

        image.GetComponent<Image>().sprite = image.colorfulImage;
    }


    public void RemoveImageFromList(ImageScript image)
    {

        if (!CheckIds(image) && !image.painted)
            return;

        imageList.Remove(image);
        RemoveFromIdList(image);

        for (int i = 0; i < buttonList.Count; i++)
        {
            if (buttonList[i].id == colorId)
            {
                Debug.Log("a");
                if (buttonList[i].ButtonHealthEvent != null)
                {
                    buttonList[i].ButtonHealthEvent();
                }

            }
        }


    }
    public void HandleButtonHealth(ButtonHealthScript bhs)
    {
        bhs.gameObject.GetComponent<Image>().fillAmount = bhs.idObjectList.Count / bhs.buttonHealth;
        Debug.Log(bhs.idObjectList.Count);
    }
    public void HandleButtonDiedAnim(ButtonHealthScript bhs)
    {
        if (bhs.idObjectList.Count <= 0)
        {

            GameObject _parentObject = bhs.transform.parent.gameObject;
            if (_parentObject != null)
            {


                Transform pos = (RectTransform)_parentObject.transform;

                LeanTween.scale(pos.gameObject, new Vector3(0f, 0f, 0f), 0.9f);
                LeanTween.moveY(pos.gameObject, pos.position.y + 1f, 0.8f);
                Destroy(_parentObject, 1f);
            }
            else
            {
                return;
            }
        }
    }
    public void AddToColorfulList(ImageScript image)
    {
        if (!CheckIds(image) && !image.painted)
            return;
        colorfulImageList.Add(image);
    }
    public void RemoveFromButtonList()
    {

        for (int i = 0; i < buttonList.Count; i++)
        {
            if (buttonList[i].idObjectList.Count <= 0)
            {

                buttonList.RemoveAt(i);

            }
        }
    }


    public void RemoveFromIdList(ImageScript image)
    {
        if (!CheckIds(image) && !image.painted)
            return;

        for (int i = 0; i < buttonList.Count; i++)
        {
            if (buttonList[i] != null)
            {

                if (buttonList[i].id == image.imageId)
                {
                    buttonList[i].idObjectList.Remove(image);
                }
            }
            else
            {
                return;
            }
        }
    }
    public void PaintObject(ImageScript image)
    {


        if (image.imageId == colorId)
        {

            image.GetComponent<Image>().material = image._objectMaterial;

            image.painted = true;
        }
    }
    public void SaveImageStatus(ImageScript image)
    {
        if (!CheckIds(image) && !image.painted)
            return;

        if (SaveGameManager.currentSave.objectIndex.Count < constantImageList.Count)
        {
            SaveGameManager.currentSave.objectIndex.Add(constantImageList.IndexOf(image));
            SaveGameManager.currentSave.SavedBoolList.Add(image.painted);

        }



    }
    public void ColorButtonClicked(ButtonHealthScript bhs)
    {
        colorId = bhs.id;
        foreach (ImageScript image in imageList)
        {


            if (image.imageId == colorId)
            {
                image.GetComponent<Image>().material = ditherMat;
            }
            else
            {
                image.GetComponent<Image>().material = normalMat;
            }

        }
    }


    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {

            SaveGameManager.Save();

        }
    }

}


