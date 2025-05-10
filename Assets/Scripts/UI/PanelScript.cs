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
    private Toggle enableAudio;
    [SerializeField]
    private Button checkButton;
    [SerializeField]
    private EmotionToggles emotionToggles;

    [SerializeField]
    private PopUp popUp;

    private Player playerRef;

    public enum PanelType
    {
        NewPlayer,
        EditPlayer,
        DeletePlayer,
        ChooseEmotions,
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

    void Start()
    {
        if (nameInputText != null)
        {
            nameInputText.onValueChanged.AddListener(delegate {
                UpdateCheckButton();
            });
        }
    }

    private void UpdateCheckButton()
    {
        if (checkButton != null)
        {
            switch (panelType)
            {
                case PanelType.NewPlayer:
                case PanelType.EditPlayer:
                    if (nameInputText.text.Length < 2) checkButton.interactable = false;
                    else checkButton.interactable = true;
                    break;
                default: break;
            }
        }
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
            EnableAudio = enableAudio.isOn,
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
        if (playerRef.Name.Equals(nameInputText.text) && playerRef.EnableAudio.Equals(enableAudio.isOn))
        {
            // gameObject.SetActive(false);
            return;
        }
        playerRef.Name = nameInputText.text;
        playerRef.EnableAudio = enableAudio.isOn;
        DBManager.Instance.UpdatePlayerToDb(playerRef);
        UIManager.Instance.RefreshSelectPlayerMenu();
        playerRef = null;
        RefreshPlayerPanel();
        // gameObject.SetActive(false);
    }

    public void DeletePlayer()
    {
        {
            // Removing photos saved
            string playerFolder = Utils.SanitizeFilePlayerName(playerRef.Name);
            string folderPath = System.IO.Path.Combine("Photos", playerFolder);
            Utils.DeleteFolderPermanently(folderPath);
        }

        DBManager.Instance.DeletePlayerFromDb(playerRef);
        UIManager.Instance.RefreshSelectPlayerMenu();
        playerRef = null;
        RefreshPlayerPanel();
        // gameObject.SetActive(false);
        UIManager.Instance.ClosePanel(PanelType.EditPlayer);
    }

    public void OpenDeletePlayerPanel()
    {
        UIManager.Instance.ShowPanelTemplate(PanelType.DeletePlayer, playerRef);
    }

    public void SaveEmotionsCheck()
    {
        GameManager.Instance.SetCurrentPlayer(playerRef);
        emotionToggles?.SaveEmotionsChecked();
        UIActions.GoToMainMenu();
    }

    public void GetPlayerInfo(Player player)
    {
        playerRef = player;
        switch (panelType)
        {
            case PanelType.EditPlayer:
                nameText.text = player.Name;
                nameInputText.text = player.Name;
                enableAudio.isOn = player.EnableAudio;
                break;
            case PanelType.DeletePlayer:
                nameText.text = "Está por eliminar a\n" + player.Name;
                break;
            case PanelType.ChooseEmotions:
                // Read selected player emotions
                emotionToggles?.LoadEmotionToggles((Emotion.EEmotions)player.EmotionsLearned);
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
                enableAudio.isOn = true;
                break;
            case PanelType.EditPlayer:
                nameText.text = string.Empty;
                nameInputText.text = string.Empty;
                enableAudio.isOn = true;
                break;
            case PanelType.DeletePlayer:
                nameText.text = string.Empty;
                break;
            case PanelType.ChooseEmotions:
                // Select all emotions by default
                emotionToggles?.LoadEmotionToggles(Emotion.EEmotions.Neutral);
                break;
            default: break;
        }
    }
}
