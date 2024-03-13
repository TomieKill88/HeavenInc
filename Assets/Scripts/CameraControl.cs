using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Camera sceneCamera;
    [SerializeField] float speed;
    [SerializeField] float yDistance;
    [SerializeField] Button upNDownButton;
    
    bool goUp;
    Vector3 minCameraPosition;
    Vector3 maxCameraPosition;
    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        goUp = true;
        minCameraPosition = sceneCamera.transform.position;
        Vector3 stepY = new Vector3(0.0f, yDistance, 0.0f);
        maxCameraPosition = minCameraPosition + stepY;
        targetPosition = Vector3.zero;
    }

    public void MoveCameraUpnDown()
    {
        if(goUp)
        {
            targetPosition = maxCameraPosition;
        }
        else
        {
            targetPosition = minCameraPosition;
        }

        upNDownButton.interactable = false;
        StartCoroutine("CameraMovement");

    }

    IEnumerator CameraMovement()
    {

        while (Vector3.Distance(sceneCamera.transform.position, targetPosition) > Mathf.Epsilon)
        {
            sceneCamera.transform.position = Vector3.MoveTowards(sceneCamera.transform.position, targetPosition, Time.deltaTime * speed);
            yield return null;
        }

        sceneCamera.transform.position = targetPosition;
        upNDownButton.interactable = true;
        goUp = !goUp;
    }
}
