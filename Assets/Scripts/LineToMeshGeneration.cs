using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineToMeshGeneration : MonoBehaviour
{
    #region Variables
    public LineRenderer line;
    public LineRenderer myLine;
    public MeshCollider meshCollider;
    public GameObject root;
    public List<GameObject> myColliders;

    public int positionsCount = 0;
    #endregion

    #region Monobehaviour
    void Update()
    {
        CreateCubes();

        myLine.positionCount = myColliders.Count;

        for (int i = 0; i < myColliders.Count; i++)
        {

            myLine.SetPosition(i, myColliders[i].transform.position);
        }

    }
    #endregion

    #region Methods
    public void UpdateMesh()
    {
        Mesh mesh = new Mesh();
        line.BakeMesh(mesh, true);
        meshCollider.sharedMesh = mesh;
    }

    public void CreateCubes()
    {

        if (line.positionCount == positionsCount)
            return;

        if (line.positionCount <= 0)
            return;

        if (myColliders.Count != line.positionCount)
        {
            foreach (var item in myColliders)
            {
                DestroyImmediate(item.gameObject);
            }
            myColliders.Clear();
        }
        else
        {
            return;
        }
        for (int i = 0; i < line.positionCount; i++)
        {
            if (i > 1)
            {
                if (Vector3.Distance(line.GetPosition(i), line.GetPosition(i - 1)) <= 0)
                {
                    i++;
                }
                else
                {
                    GameObject go = new GameObject();
                    myColliders.Add(go);
                    go.transform.parent = root.transform;
                    BoxCollider box = go.AddComponent<BoxCollider>();
                    Vector3 pos = line.GetPosition(i);
                    pos.z = 0;
                    go.transform.localPosition = pos;
                    box.size = new Vector3((line.GetPosition(i) - line.GetPosition(i - 1)).magnitude, 0.2f, 0.2f);

                    float temp = Vector3.Angle(line.GetPosition(i) - line.GetPosition(i - 1), this.transform.parent == null ? this.transform.forward : this.transform.parent.forward);

                    go.transform.localEulerAngles = new Vector3(0, 0, temp);


                    go.name = line.GetPosition(i) + ", " + line.GetPosition(i - 1) + ", " + go.transform.eulerAngles;
                }
            }
        }


        if (myColliders.Count < 1)
            return;

        Vector3 recenter = FindCenterOfTransforms(myColliders);

        recenter.x *= -1;
        recenter.y = (-1 * recenter.y) - FindYOfTransforms(myColliders);
        recenter.z = 0;

        root.transform.localPosition = recenter;

        positionsCount = line.positionCount;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < myColliders.Count; i++)
        {
            if (i == 0)
            { }
            else

                Gizmos.DrawLine(myColliders[i].transform.position, myColliders[i - 1].transform.position);
        }
    }


    public Vector3 FindCenterOfTransforms(List<GameObject> transforms)
    {
        var bound = new Bounds(transforms[0].transform.localPosition, Vector3.zero);
        for (int i = 1; i < transforms.Count; i++)
        {
            bound.Encapsulate(transforms[i].transform.localPosition);
        }
        return bound.center;
    }


    public float FindYOfTransforms(List<GameObject> transforms)
    {
        var bound = new Bounds(transforms[0].transform.localPosition, Vector3.zero);
        for (int i = 1; i < transforms.Count; i++)
        {
            bound.Encapsulate(transforms[i].transform.localPosition);
        }
        return bound.extents.y;
    }
    #endregion
}