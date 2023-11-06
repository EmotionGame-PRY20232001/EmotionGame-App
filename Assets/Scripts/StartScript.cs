using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{

    [SerializeField]
    private GameObject scrollViewPrefab;
    [SerializeField]
    private GameObject newPlayerButton;
    [SerializeField]
    private GameObject newPlayerButton2;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        //int randNum = Random.Range(0, 2);
        int randNum = 0;
        if (randNum  == 0)
        { 
            Instantiate(scrollViewPrefab, transform);
            Instantiate(newPlayerButton2, transform);
        }
        else
        {
            Instantiate(newPlayerButton, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
