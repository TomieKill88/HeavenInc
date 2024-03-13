using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    //************ SERIALIZED VARIABLES ********//
    [SerializeField] Image bgImage;
    [SerializeField] Sprite bgSprite;

    [SerializeField] Button phoneButton;
    [SerializeField] Button deliverButton;
    [SerializeField] Button dialogueButton;

    [SerializeField] Sprite diagBtnSpriteHidden;
    [SerializeField] Sprite diagBtnSpriteNext;
    [SerializeField] Sprite diagBtnSpriteHey;

    [SerializeField] Sprite phoneBtnSpriteIdle;
    [SerializeField] Sprite phoneBtnSpriteMsg;
    [SerializeField] Sprite phoneBtnSpriteAberration;

    //************ UNITY OBJECTS ***************//
    //************ VARIABLES *******************//

    //************ PROPERTIES ******************//


    // Start is called before the first frame update
    public void Init()
    {
        // INIT DIALOGUE BOXES
        BackgroundSprite(bgSprite);
        CellphoneStateChange(CellphoneBtnState.IDLE);
        DialogueStateChange(DialogueBtnState.HIDDEN);
    }

    public void BackgroundSprite(Sprite bgSprite)
    {
        bgImage.sprite = bgSprite; 
    }

    public void DialogueStateChange(DialogueBtnState dialState)
    {
        if (dialState == DialogueBtnState.NEXT)
        {
            dialogueButton.interactable = true;
            dialogueButton.GetComponent<Image>().sprite = diagBtnSpriteHey;
        }
        else if (dialState == DialogueBtnState.HEY)
        {
            dialogueButton.interactable = true;
            dialogueButton.GetComponent<Image>().sprite = diagBtnSpriteHey;
        }
        else
        {
            dialogueButton.interactable = false;
            dialogueButton.GetComponent<Image>().sprite = diagBtnSpriteHidden;
        }
    }

    public void CellphoneStateChange(CellphoneBtnState phoneState)
    {
        if (phoneState == CellphoneBtnState.ABERRATION)
            phoneButton.GetComponent<Image>().sprite = phoneBtnSpriteAberration;        
        else if(phoneState == CellphoneBtnState.MSG)
            phoneButton.GetComponent<Image>().sprite = phoneBtnSpriteMsg;
        else
            phoneButton.GetComponent<Image>().sprite = phoneBtnSpriteIdle;
    }
}

[System.Serializable]
public enum CellphoneBtnState : ushort
{
    IDLE = 0,
    MSG,
    ABERRATION
}

[System.Serializable]
public enum DialogueBtnState : ushort
{
    HIDDEN = 0,
    HEY,
    NEXT
}
