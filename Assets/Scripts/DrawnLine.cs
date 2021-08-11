using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawnLine : MonoBehaviour
{

    #region Variables
    public LineRenderer line; // Assign via inspector

    //   public GameObject test;
    public bool isDrawing;
    public int count = 0;

    public List<Vector3> savedPos;

    public MouseToos mouse;

    #endregion

    #region Monobehaviou
    void Update()
    {//aqui jala sin moviemiento
        if (isDrawing)
        {

            if (Input.touchCount < 1 || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Debug.Log("goodbye");

                StartCoroutine(Fade());

                line.positionCount = 0;
                isDrawing = false;
                return;
            }

            if (mouse.GetPositionTouch() == Vector3.zero)
                return;

            savedPos.Add(mouse.GetPositionTouch());


            line.positionCount = savedPos.Count;

            for (int i = 0; i < savedPos.Count; i++)
            {
                Vector3 cam = Camera.main.transform.position;

                cam.z = 0;

                cam.y -= 3.8f;

                line.SetPosition(i, savedPos[i] + cam);
            }


        }
        else
        {
            if (Input.touchCount > 0)
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Debug.Log("hello");
                    StopAllCoroutines();
                    line.material.SetColor("_Color", new Color(line.material.color.r, line.material.color.g, line.material.color.b, 1));
                    isDrawing = true;
                    savedPos.Clear();

                    if (mouse.GetPositionTouch() == Vector3.zero)
                        return;

                    savedPos.Add(mouse.GetPositionTouch());

                }
        }
    }
    #endregion

    #region  Methods

    public IEnumerator Fade()
    {
        float timer = 0;
        while (timer < 2.0f)
        {
            timer += Time.deltaTime;

            line.material.SetColor("_Color", new Color(line.material.color.r, line.material.color.g, line.material.color.b, Mathf.Lerp(1, 0, timer / 1f)));
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < savedPos.Count; i++)
        {
            if (i == 0)
            { }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(savedPos[i], savedPos[i - 1]);
            }
        }
    }
    #endregion

}
