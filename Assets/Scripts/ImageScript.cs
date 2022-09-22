using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{
    public int imageId;
    [SerializeField]
    public Material _objectMaterial { get; private set; }
    
    public Sprite colorfulImage;
    public bool painted;
    private bool _onetimeBool;
   



    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(PaintSelf);
        //GameManager.onPaint += SetColor;

    }
    private void OnDisable()
    {
        //GameManager.onPaint -= SetColor;
    }

    private void Start()
    {



        _objectMaterial = GetComponent<Image>().material;

        if (painted)
        {
            GameManager.onPaint?.Invoke(this);

        }
    }
    public void PaintSelf()
    {

        GameManager.onPaint?.Invoke(this);

    }
    


    //public void PaintSelf()
    //{


    //    if (imageId == _gameManager.colorId)
    //    {
    //        painted = true;


    //        gameObject.GetComponent<Image>().material = _objectMaterial;

    //        SaveGameManager.currentSave.SavedBoolList.Add(painted);
    //        if (GameManager.onPaint != null)
    //            GameManager.onPaint(this);




    //        //Image ChildImage = this.gameObject.transform.GetChild(0).GetComponent<Image>();
    //        //Color32 tempChildColor = ChildImage.color;
    //        //this.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    //        //this.gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color32(tempChildColor.r, tempChildColor.g, tempChildColor.b, 255);
    //    }
    //}
    private void Update()
    {

    }

    //public void SetColor(ImageScript imageScr)
    //{

    //    Debug.Log("here");
    //    SaveGameManager.currentSave.objectIndex.Add(_gameManager.constantImageList.IndexOf(imageScr));
    //    imageScr.GetComponent<Image>().sprite = imageScr.colorfulImage;











    //}





}
