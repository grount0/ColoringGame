using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    Vector3 viewPos;
    public Camera MainCamera;
    [SerializeField] Vector3 screenBounds;
    private bool _doubleTouch;
    public float zoomOutMin, zoomOutMax;
    public GameObject zoomObject;
    Vector3 velocity = Vector3.zero;
    [SerializeField] float smoothDamping;
    private void Start()
    {

    }
    private void Update()
    {
        ZoomFinger();
        SetZoomButtonActive();
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
    public void DragHandler(BaseEventData data)
    {
        if (!_doubleTouch)
        {

            PointerEventData pointerData = (PointerEventData)data;
            var vertExtent = Camera.main.orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height;
            Vector2 position;
            var tempPos = (RectTransform)gameObject.transform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                pointerData.position,
                canvas.worldCamera,
                out position);
            Vector3 stageDimensions = new Vector3(horzExtent, vertExtent);
            transform.position =Vector3.SmoothDamp(transform.position, canvas.transform.TransformPoint(position),ref velocity,smoothDamping);
        }






    }
    public void ZoomFinger()
    {
        if (Input.touchCount == 2)
        {
            _doubleTouch = true;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitue = (touchZero.position - touchOne.position).magnitude;
            float difference = currentMagnitue - prevMagnitude;
            zoom(difference * 0.01f);

        }
        else if (Input.touchCount <= 1)
        {
            _doubleTouch = false;
        }
    }
    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);

    }

    public void SetZoomButtonActive()
    {
        if (Camera.main.orthographicSize != zoomOutMax)
        {
           
            zoomObject.SetActive(true);
        }
        else
        {
            zoomObject.SetActive(false);
        }
    }
    public void SetZoomButtonDeActive()
    {
        zoomObject.SetActive(false);
    }



    public void ZoomOutWhenPressed()
    {
        Camera.main.orthographicSize = zoomOutMax;
       
    }


}

