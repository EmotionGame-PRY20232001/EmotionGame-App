using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerButton : MonoBehaviour
{
    public void OpenNewPlayerPanel()
    {
        UIManager.Instance.ShowPanelTemplate();
    }
}
