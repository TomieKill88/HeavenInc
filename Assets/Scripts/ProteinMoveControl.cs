using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProteinMoveControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    // Objects
    private Image objImage;
    private RectTransform objRectTranform;

    // Rotate variables
    float rotateDirection;
    bool isRotating;

    // OnClick variables
    private bool isSelected;
    private bool wasDragged;

    // OnDrag variables
    private Vector3 mouseInitPosition;
    private Vector3 objInitPosition;


    // Start is called before the first frame update
    void Start()
    {
        objImage = GetComponent<Image>();
        objRectTranform = GetComponent<RectTransform>();
        isSelected = false;
        wasDragged = false;
        isRotating = false;
    }

    void Update()
    {
        rotateDirection = Input.GetAxisRaw("Horizontal");
       
        if (Mathf.Abs(rotateDirection) > Mathf.Epsilon) 
        {
            if (isSelected && !isRotating)
            {
                isRotating = true;
                rotateObject();
            }
        }
        else
        {
            isRotating = false;
        }        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!wasDragged)
        {
            isSelected = !isSelected;

            if (isSelected)
            {
                objImage.color = new Color32(255, 255, 255, 170);
            }
            else
            {
                objImage.color = new Color32(255, 255, 255, 255);
            }
        }        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isSelected)
        {
            objImage.color = new Color32(170, 170, 255, 170);
            mouseInitPosition = Input.mousePosition;
            objInitPosition = objRectTranform.localPosition;
            wasDragged = true;
        }            
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isSelected)
        {
            Vector3 mouseCanvasRelative = Input.mousePosition - mouseInitPosition;
            objRectTranform.localPosition = objInitPosition + mouseCanvasRelative;         
        }        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isSelected)
        {
            objImage.color = new Color32(255, 255, 255, 170);
            wasDragged = false;
        }        
    }

    public void rotateObject()
    {
        if (rotateDirection > 0.0f)
        {
            objRectTranform.Rotate(new Vector3(0.0f, 0.0f, -90.0f));
        }
        else
        {
            objRectTranform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
        }
    }


}
