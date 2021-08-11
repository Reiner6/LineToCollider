using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseToos : MonoBehaviour
{
    #region Variables
    public bool debugMousePosition = false;
    #endregion

    #region MonoBehaviour
    void Update()
    {
        if (debugMousePosition)
            GetPositionMouse();
    }
    #endregion

    #region Methods
    /*
     * Get touch position based on touch(0) position over the RawImage on the screen
     */
    public Vector3 GetPositionTouch()
    {

        Vector3[] corners = new Vector3[4];
        GetComponent<RawImage>().rectTransform.GetWorldCorners(corners);
        Rect newRect = new Rect(corners[0], corners[2] - corners[0]);
        // Debug.Log(newRect.Contains(Input.mousePosition));

        if (Input.touchCount > 0)
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                if (newRect.Contains(Input.GetTouch(0).position))
                {
                    GetComponent<RawImage>().rectTransform.GetWorldCorners(corners);
                    newRect = new Rect(corners[0], corners[2] - corners[0]);

                    float xPositionDeltaPoint = Input.GetTouch(0).position.x - newRect.x;
                    float yPositionDeltaPoint = Input.GetTouch(0).position.y - newRect.y;


                    float xPositionCameraCoordinates = (xPositionDeltaPoint / GetComponent<RawImage>().rectTransform.sizeDelta.x);
                    float yPositionCameraCoordinates = (yPositionDeltaPoint / GetComponent<RawImage>().rectTransform.sizeDelta.y);

                    if (xPositionCameraCoordinates > 0.5f)
                        xPositionCameraCoordinates -= 0.5f;
                    else
                        xPositionCameraCoordinates -= 0.5f;

                    if (yPositionCameraCoordinates > 0.5f)
                        yPositionCameraCoordinates -= 0.5f;
                    else
                        yPositionCameraCoordinates -= 0.5f;

                    xPositionCameraCoordinates *= 2f;
                    yPositionCameraCoordinates *= 2f;

                    return new Vector3(xPositionCameraCoordinates, yPositionCameraCoordinates, 0);
                }
        return Vector3.zero;
    }

    /*
     * Debug function for testing cursor position over RawImage on screen
     */
    public Vector3 GetPositionMouse()
    {

        Vector3[] corners = new Vector3[4];
        GetComponent<RawImage>().rectTransform.GetWorldCorners(corners);
        Rect newRect = new Rect(corners[0], corners[2] - corners[0]);
        // Debug.Log(newRect.Contains(Input.mousePosition));

        if (newRect.Contains(Input.mousePosition))
        {
            GetComponent<RawImage>().rectTransform.GetWorldCorners(corners);
            newRect = new Rect(corners[0], corners[2] - corners[0]);

            float xPositionDeltaPoint = Input.mousePosition.x - newRect.x;
            float yPositionDeltaPoint = Input.mousePosition.y - newRect.y;

            Debug.Log("The x position delta is: " + xPositionDeltaPoint);
            Debug.Log("The y position delta is: " + yPositionDeltaPoint);

            //The value "170" is the raw image size currently
            float xPositionCameraCoordinates = (xPositionDeltaPoint / GetComponent<RawImage>().rectTransform.sizeDelta.x);
            float yPositionCameraCoordinates = (yPositionDeltaPoint / GetComponent<RawImage>().rectTransform.sizeDelta.y);

            if (xPositionCameraCoordinates > 0.5f)
                xPositionCameraCoordinates -= 0.5f;
            else
                xPositionCameraCoordinates -= 0.5f;

            if (yPositionCameraCoordinates > 0.5f)
                yPositionCameraCoordinates -= 0.5f;
            else
                yPositionCameraCoordinates -= 0.5f;

            Debug.Log(xPositionCameraCoordinates + ", " + yPositionCameraCoordinates);
            return new Vector3(xPositionCameraCoordinates, yPositionCameraCoordinates, 0);
        }
        return Vector3.zero;
    }
    #endregion
}