using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;

public class ButtonHealthScript : MonoBehaviour
{
    public int id;
    private GameManager _gameManager;
    public List<ImageScript> idObjectList;
    public List<ImageScript> staticIdObjectList;
    public float buttonHealth;
    [SerializeField] private GameObject _parentObject;
    [SerializeField] private UDictionaryExample buttonDictionary;
    [SerializeField] private TextMeshProUGUI idText;


    private int idNumber;
    private string numbersOnly;

    public delegate void ButtonHealth();
    public ButtonHealth ButtonHealthEvent;

    public delegate void ButtonAnimation();
    public  ButtonAnimation OnButtonDied;


    GameObject parentObj;

    public List<ImageScript> sameIdObjectList;

    private void OnEnable()
    {
        ButtonHealthEvent += HandleButtonHealth;
        OnButtonDied += HandleButtonDiedAnimation;
        
    }
    private void OnDisable()
    {
       
        ButtonHealthEvent -= HandleButtonHealth;
        OnButtonDied -= HandleButtonDiedAnimation;
    }
    private void Awake()
    {

    }

    void Start()
    {

        _parentObject = transform.parent.gameObject;
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        numbersOnly = Regex.Replace(gameObject.transform.parent.gameObject.name, "[^0-9]", "");
        idNumber = Convert.ToInt32(numbersOnly);
        idNumber += 1;
        idText.text = idNumber + "";
        id = idNumber;
        buttonDictionary = GameObject.FindObjectOfType<UDictionaryExample>();
        
        foreach (var item in buttonDictionary.dictionary1)
        {
            item.Key.Object.transform.GetChild(0).GetComponent<Image>().color = new Color32(item.Value.ForegroundColor.r, item.Value.ForegroundColor.g, item.Value.ForegroundColor.b, item.Value.ForegroundColor.a);


            item.Key.Object.GetComponent<Image>().color = new Color32(item.Value.BackgroundColor.r, item.Value.BackgroundColor.g, item.Value.BackgroundColor.b, item.Value.BackgroundColor.a);
        }




        this.GetComponent<Button>().onClick.AddListener(OnClick);
        foreach (ImageScript image in _gameManager.imageList)
        {
            if (image.imageId == id && !image.painted)
            {
                idObjectList.Add(image);
            }
        }
        foreach(ImageScript image in _gameManager.constantImageList)
        {
            if(image.imageId==id)
            {
                sameIdObjectList.Add(image);
            }
        }
        
        buttonHealth = sameIdObjectList.Count;
        gameObject.GetComponent<Image>().fillAmount = buttonHealth;


       

        if (idObjectList.Count <= 0)
        {

            if (OnButtonDied != null)
            {
                OnButtonDied();
                


            }

        }
        if (ButtonHealthEvent != null)
            ButtonHealthEvent();



    }


    void Update()
    {
        //if (idObjectList.Count <= 0)
        //{

        //    if (OnButtonDied != null)
        //    {
        //        OnButtonDied();
        //        _gameManager.buttonList.Remove(this);


        //    }

        //}
    }

    void HandleButtonHealth()
    {

        Debug.Log("HERE");
        gameObject.GetComponent<Image>().fillAmount = idObjectList.Count / buttonHealth;
        if (idObjectList.Count <= 0)
        {

            if (OnButtonDied != null)
            {
                OnButtonDied();
                //_gameManager.buttonList.Remove(this);


            }

        }

    }

    

    void HandleButtonDiedAnimation()
    {

        if (_parentObject != null)
        {

            
            Transform pos = (RectTransform)_parentObject.transform;

            LeanTween.scale(pos.gameObject, new Vector3(0f, 0f, 0f), 0.9f);
            LeanTween.moveY(pos.gameObject, pos.position.y + 1f, 0.8f);
            _gameManager.buttonList.Remove(this);
            Destroy(_parentObject, 1f);
        }
        else
        {
            return;
        }

    }

    void OnClick()
    {


        GameManager.onColorButtonTouched?.Invoke(this);



        //_gameManager.colorId = id;

        //foreach (ImageScript image in _gameManager.imageList)
        //{
        //    if (image.imageId == _gameManager.colorId)
        //    {
        //        image.GetComponent<Image>().material = _gameManager.ditherMat;
        //    }
        //    else
        //    {
        //        image.GetComponent<Image>().material = _gameManager.normalMat;
        //    }
        //}

        //if (idObjectList.Count == 0)
        //{
        //    if (OnButtonDied != null)
        //    {
        //        OnButtonDied();
        //    }
        //}


    }

}
