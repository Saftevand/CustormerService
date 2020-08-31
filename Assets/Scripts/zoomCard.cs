using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomCard : MonoBehaviour
{
    public GameObject Canvas;

    private GameObject _zoomCard;

    public void Awake()
    {
        Canvas = GameObject.Find("Canvas");
    }

    /*
    public void OnMouseDown()
    {
        Debug.Log("Click");
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Right click");
            _zoomCard = Instantiate(gameObject, new Vector2(200, 300), Quaternion.identity);
            _zoomCard.transform.SetParent(Canvas.transform, false);
            _zoomCard.layer = LayerMask.NameToLayer("Zoom");

            RectTransform rect = _zoomCard.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(240, 360);
        }
    }
    public void OnHoverEnter()
    {
        
        _zoomCard = Instantiate(gameObject, new Vector2(200, 300), Quaternion.identity);
        _zoomCard.transform.SetParent(Canvas.transform, false);
        _zoomCard.layer = LayerMask.NameToLayer("Zoom");

        RectTransform rect = _zoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(240, 360);
       
        
    }

    public void OnHoverExit()
    {
        Destroy(_zoomCard);
    }

    */
}
