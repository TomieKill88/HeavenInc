using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AminoAcidController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    //*************** SERIALIZED VARIABLES *******************//
    [SerializeField] AminoAcidID aminoAcidID;
    [SerializeField] Sprite aminoAcidSprite;

    //*************** UNITY OBJECTS *******************//
    private Image objImage;
    private RectTransform objRectTranform;
    private Transform parentAfterDrag;

    //*************** VARIABLES *******************//
    // Rotate variables
    float aminoAcidOrientation;
    float rotateDirection;
    bool isRotating;

    // OnClick variables
    private bool isSelected;
    private bool wasDragged;

    // OnDrag variables
    private Vector3 mouseInitPosition;
    private Vector3 objInitPosition;

    //*************** PROPERTIES *******************//
    public int CurrentBondID { get; set; }

    public Transform ParentAfterDrag
    {
        get { return parentAfterDrag; }
        set { parentAfterDrag = value; }
    }

    public AminoAcidID AminoAcidID
    {
        get { return aminoAcidID; }
        set { aminoAcidID = value; }
    }

    public Sprite AminoAcidSprite
    {
        get { return aminoAcidSprite; }
        set { aminoAcidSprite = value; }
    }

    public float AminoAcidOrientation
    {
        get { return aminoAcidOrientation; }
        set { aminoAcidOrientation = value; }
    }
    
    //*************** UNITY INTERFACES *******************//
    void Awake()
    {
        objImage = GetComponent<Image>();
        objRectTranform = GetComponent<RectTransform>();

        aminoAcidOrientation = objRectTranform.localRotation.eulerAngles.z;

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
        }            
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isSelected)
        {
            // Movement
            Vector3 mouseCanvasRelative = Input.mousePosition - mouseInitPosition;
            objRectTranform.localPosition = objInitPosition + mouseCanvasRelative;           
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
        }        
    }


    //*************** MEMBER METHODS *******************//
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

        aminoAcidOrientation = objRectTranform.localRotation.eulerAngles.z;
    }

    public void UpdateSprite(bool rotate)
    {
        objImage.sprite = aminoAcidSprite;
        if (rotate)
        {
            objRectTranform.Rotate(new Vector3(0.0f, 0.0f, aminoAcidOrientation - objRectTranform.localRotation.eulerAngles.z));
        }            
    }

    public void UpdateAminoAcid(AminoAcidController newData)
    {
        this.AminoAcidID = newData.AminoAcidID;
        this.AminoAcidSprite = newData.AminoAcidSprite;
        UpdateSprite(false);
        objRectTranform.Rotate(new Vector3(0.0f, 0.0f, newData.AminoAcidOrientation - objRectTranform.localRotation.eulerAngles.z));
        this.AminoAcidOrientation = objRectTranform.localRotation.eulerAngles.z;
    }

    public AminoAcidController DeepCopy()
    {
        AminoAcidController other = (AminoAcidController)this.MemberwiseClone();
        other.AminoAcidID = aminoAcidID;
        other.CurrentBondID = CurrentBondID;
        other.AminoAcidOrientation = aminoAcidOrientation;
        other.AminoAcidSprite = aminoAcidSprite;
        return other;
    }

    public string AminoacidToString()
    {
        return "AminoacidID: " + AminoAcidID + " in Bond #" + CurrentBondID + ". Rotation: " + aminoAcidOrientation + ". Sprite:" + aminoAcidSprite.name;
    }
}
