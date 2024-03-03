using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProteinMoveControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image objImage;
    private RectTransform objRectTranform;
    private Vector3 mouseInitPosition;
    private Vector3 objInitPosition;


    // Start is called before the first frame update
    void Start()
    {
        objImage = GetComponent<Image>();
        objRectTranform = GetComponent<RectTransform>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        objImage.color = new Color32(255,255,255,170);
        mouseInitPosition = Input.mousePosition;
        objInitPosition = objRectTranform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mouseCanvasRelative = Input.mousePosition - mouseInitPosition;

        objRectTranform.localPosition = objInitPosition + mouseCanvasRelative;

        Debug.Log(Input.mousePosition);
        Debug.Log("objRectTranform.position" + objRectTranform.localPosition + mouseCanvasRelative);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        objImage.color = new Color32(255, 255, 255, 255);
    }



}
