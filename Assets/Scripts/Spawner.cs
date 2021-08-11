using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Variables
    public static Spawner instance;

    public List<GameObject> prefabs;
    public float min;
    public float max;
    bool pausePrefabs = false;
    #endregion

    #region Monobehaviour
    public void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {

        while (true)
        {
            while (!pausePrefabs)
            {
                yield return new WaitForSeconds(Random.Range(min, max));

                GameObject go =

                Instantiate(prefabs[Random.Range(0, prefabs.Count)]);


                go.transform.position = this.transform.position;
            }
            yield return null;
        }

    }

    public void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(2, 0, 0);
    }

    #endregion
}