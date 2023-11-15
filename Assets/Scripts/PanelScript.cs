using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameText;
    [SerializeField]
    private Toggle needsText;

    public void CloseNewPlayerPanel()
    {
        RefreshNewPlayerPanel();
        gameObject.SetActive(false);
    }

    public void AddNewPlayer()
    {
        Player player = new Player
        {
            Name = nameText.text,
            NeedsText = needsText.isOn,
            BackgroundId = 0
        };
        DBManager.Instance.AddPlayerToDb(player);
        UIManager.Instance.RefreshSelectPlayerMenu();
        RefreshNewPlayerPanel();
        gameObject.SetActive(false);
    }

    private void RefreshNewPlayerPanel()
    {
        nameText.text = string.Empty;
        needsText.isOn = false;
    }
}
