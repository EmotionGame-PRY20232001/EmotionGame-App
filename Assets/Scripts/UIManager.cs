using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject newPlayerTemplate;

    public void ShowPanelTemplate()
    {
        newPlayerTemplate.SetActive(true);
    }
}
