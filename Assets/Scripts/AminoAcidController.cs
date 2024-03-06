using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProteinMoveControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] float initYPosition;

    // Objects
    private Image objImage;
    private RectTransform objRectTranform;
    private Transform parentAfterDrag;

    // Rotate variables
    float rotateDirection;
    bool isRotating;

    // OnClick variables
    private bool isSelected;
    private bool wasDragged;

    // OnDrag variables
    private Vector3 mouseInitPosition;
    private Vector3 objInitPosition;


    // Properties
    public Transform ParentAfterDrag
    {
        get { return parentAfterDrag; }
        set { parentAfterDrag = value; }
    }


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

            Debug.Log("objInitPosition " + objRectTranform.localPosition);

            if (isSelected)
            {
                Debug.Log(initYPosition + " Was selected");
                objImage.color = new Color32(255, 255, 255, 170);
            }
            else
            {
                Debug.Log(initYPosition + " Was deselected");
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
            wasDragged = true;


            // DRAG AND DROP
            // We get the parent so we can return to the slot if we drop somewhere invalid
            parentAfterDrag = transform.parent;
            // We assign the canvas as new parent so we can draw the dragged item infront of everyone in the canvas. We also need to set it as
            // the last sibling to be drawn for this
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            // Lastly we deactivate Raycast so the Item doesn't drop into itself
            objImage.raycastTarget = false;

            // Init here so we get the position with the canvas as parent
            objInitPosition = objRectTranform.localPosition;
            Debug.Log(initYPosition + " Begings drag");
            Debug.Log("objInitPosition " + objInitPosition);
        }            
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isSelected)
        {
            // Movement
            Vector3 mouseCanvasRelative = Input.mousePosition - mouseInitPosition;
            objRectTranform.localPosition = objInitPosition + mouseCanvasRelative;

            Debug.Log("mouserelative " + mouseCanvasRelative);            
        }
        else
        {
            // This stops OnDrop(PointerEventData eventData) from being called.
            eventData.pointerDrag = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isSelected)
        {
            objImage.color = new Color32(255, 255, 255, 170);
            wasDragged = false;


            // DRAG AND DROP
            // We return parenthood to original (or new) parent
            transform.SetParent(parentAfterDrag);
            // reactivate Raycast so the mouse can interact with it
            objImage.raycastTarget = true;

            Debug.Log(initYPosition + " Ends drag");
            Debug.Log("objInitPosition " + objInitPosition);
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
