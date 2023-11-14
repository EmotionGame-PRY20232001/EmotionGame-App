using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

        var playerList = DBManager.Instance.GetPlayersFromDb();
        if (playerList.Count  != 0)
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
