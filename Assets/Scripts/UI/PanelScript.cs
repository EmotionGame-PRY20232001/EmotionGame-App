using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PopUp))]
public class PanelScript : MonoBehaviour
{
    [SerializeField]
    private PanelType panelType;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_InputField nameInputText;
    [SerializeField]
    private Toggle needsText;

    [SerializeField]
    private PopUp popUp;

    private Player playerRef;

    private enum PanelType
    {
        NewPlayer,
        EditPlayer,
        DeletePlayer
    }

    public void Awake()
    {
        popUp = gameObject.GetComponent<PopUp>();
        // popUp.onClose += ClosePlayerPanel;
        popUp.onClose += RefreshPlayerPanel;

        switch (panelType)
        {
            case PanelType.NewPlayer:
                popUp.onAccept += AddNewPlayer;
                break;
            case PanelType.EditPlayer:
                popUp.onAccept += UpdatePlayer;
                break;
            case PanelType.DeletePlayer:
                popUp.onAccept += DeletePlayer;
                break;
            default: break;
        }
        // popUp.onOpen += RefreshPlayerPanel;
    }

    // public void ClosePlayerPanel()
    // {
    //     RefreshPlayerPanel();
    //     // gameObject.SetActive(false);
    // }

    public void AddNewPlayer()
    {
        Player player = new Player
        {
            Name = nameInputText.text,
            NeedsText = needsText.isOn,
            BackgroundId = 0,
            // GuideId = 0,
            GuideJSON = Character.Custom.GetDefault().ToJson()
        };
        DBManager.Instance.AddPlayerToDb(player);
        UIManager.Instance.RefreshSelectPlayerMenu();
        RefreshPlayerPanel();
        // gameObject.SetActive(false);
    }

    public void UpdatePlayer()
    {
        if (playerRef.Name.Equals(nameInputText.text) && playerRef.NeedsText.Equals(needsText.isOn))
        {
            // gameObject.SetActive(false);
            return;
        }
        playerRef.Name = nameInputText.text;
        playerRef.NeedsText = needsText.isOn;
        DBManager.Instance.UpdatePlayerToDb(playerRef);
        UIManager.Instance.RefreshSelectPlayerMenu();
        playerRef = null;
        RefreshPlayerPanel();
        // gameObject.SetActive(false);
    }

    public void DeletePlayer()
    {
        DBManager.Instance.DeletePlayerFromDb(playerRef);
        UIManager.Instance.RefreshSelectPlayerMenu();
        playerRef = null;
        RefreshPlayerPanel();
        // gameObject.SetActive(false);
        UIManager.Instance.ClosePanel("Edit Player");
    }

    public void OpenDeletePlayerPanel()
    {
        UIManager.Instance.ShowPanelTemplate("Delete Player", playerRef);
    }

    public void GetPlayerInfo(Player player)
    {
        playerRef = player;
        switch (panelType)
        {
            case PanelType.EditPlayer:
                nameText.text = player.Name;
                nameInputText.text = player.Name;
                needsText.isOn = player.NeedsText;
                break;
            case PanelType.DeletePlayer:
                nameText.text = "Está por eliminar a\n" + player.Name;
                break;
            default: break;
        }
    }

    private void RefreshPlayerPanel()
    {
        switch (panelType)
        {
            case PanelType.NewPlayer:
                nameInputText.text = string.Empty;
                needsText.isOn = false;
                break;
            case PanelType.EditPlayer:
                nameText.text = string.Empty;
                nameInputText.text = string.Empty;
                needsText.isOn = false;
                break;
            case PanelType.DeletePlayer:
                nameText.text = string.Empty;
                break;
            default: break;
        }
    }
}
