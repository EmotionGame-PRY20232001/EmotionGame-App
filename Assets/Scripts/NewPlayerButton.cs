using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerButton : MonoBehaviour
{
    public void OpenNewPlayerPanel()
    {
        GameObject.Find("UI Manager").GetComponent<UIManager>().ShowPanelTemplate();
    }
}
