using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] float letterSpeed;

    //************ UNITY OBJECTS ***************//
    Image dialogBoxImage;

    //************ VARIABLES *******************//
    bool isHidden;

    //************ PROPERTIES ******************//
    bool IsHidden
    {
        get { return isHidden; }
    }

    public void Init()
    {
        dialogBoxImage = GetComponent<Image>();
        dialogueText.text = "";
        isHidden = false;
    }

    public void SetDialogue(string dialogue)
    {
        if(isHidden)
            HideBox(false);

        dialogueText.text = "";
        dialogueText.text = dialogue;
    }

    public IEnumerator TypeDialogue(string dialogue)
    {
        if (isHidden)
            HideBox(false);

        dialogueText.text = "";

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1 / letterSpeed);
        }
    }

    public void HideBox(bool hide)
    {
        if (hide)
        {
            dialogueText.text = "";
            dialogBoxImage.color = Color.clear;
        }
        else 
        {
            dialogBoxImage.color = new Color(0.2f, 0.2f, 0.2f, 0.5f) ;
        }
        
        isHidden = hide;
    }
}
