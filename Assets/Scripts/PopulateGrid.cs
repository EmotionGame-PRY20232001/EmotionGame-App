using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateGrid : MonoBehaviour
{

    public GameObject prefab;
    public int numInstances;

    // Start is called before the first frame update
    void Start()
    {
        Populate();
    }

    void Populate()
    {
        GameObject newObj;

        for (int i = 0; i < numInstances; i++)
        {
            newObj = (GameObject)Instantiate(prefab, transform);
        }
    }
}
