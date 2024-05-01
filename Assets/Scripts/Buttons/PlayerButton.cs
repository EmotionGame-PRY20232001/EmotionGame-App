using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private Image background;
    [SerializeField]
    private Character guide;
    [SerializeField]
    private bool needsText;
    [SerializeField]
    private GameObject editButton;
    [SerializeField]
    protected ThemeButton playerButton;

    private Player playerReference;

    void Start()
    {
        if (playerButton != null)
        {
            playerButton.onNormalState += DesactivateEdit;
        }
    }

    public void ButtonInstantiate(Player player)
    {
        playerReference = player;
        nameText.text = player.Name;
        background.sprite = GameManager.Instance.GetBackgrounds()[(Theme.EBackground)player.BackgroundId].Texture;
        guide.SetByJson(player.GuideJSON);
        needsText = player.NeedsText;
    }

    public void ActivateEdit()
    {
        if (editButton.activeSelf)
        {
            GameManager.Instance.SetCurrentPlayer(playerReference);
            SceneManager.LoadScene("MainMenu");
        }
        editButton.SetActive(true);
    }

    public void DesactivateEdit()
    {
        if (editButton != null && editButton.activeSelf)
        {
            editButton.SetActive(false);
        }
    }

    public void OpenEditPlayerPanel()
    {
        UIManager.Instance.ShowPanelTemplate("Edit Player", playerReference);
    }
}
