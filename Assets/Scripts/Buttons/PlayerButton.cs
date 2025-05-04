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
    private GameObject optionsContainer;
    [SerializeField]
    protected ThemeButton playerButton;
    [SerializeField]
    private Image playIcon;

    private Player playerReference;

    void Start()
    {
        if (playerButton != null)
        {
            playerButton.onNormalState += DesactivateOptions;
        }
    }

    public void ButtonInstantiate(Player player)
    {
        playerReference = player;
        nameText.text = player.Name;
        background.sprite = GameManager.Instance.GetBackgrounds()[(Theme.EBackground)player.BackgroundId].Texture;
        guide.SetByJson(player.GuideJSON);
        playIcon?.CrossFadeAlpha(0.0f, 0.0f, true);
    }

    public void ActivateOptions()
    {
        if (optionsContainer.activeSelf)
        {
            ChooseEmotions();
            //GameManager.Instance.SetCurrentPlayer(playerReference);
            //SceneManager.LoadScene("MainMenu");
        }
        optionsContainer.SetActive(true);
        playIcon?.CrossFadeAlpha(1.0f, 0.25f, true);
    }

    public void DesactivateOptions()
    {
        if (optionsContainer != null && optionsContainer.activeSelf)
        {
            optionsContainer.SetActive(false);
            playIcon?.CrossFadeAlpha(0.0f, 0.25f, true);
        }
    }


    //// Options
    public void OpenEditPlayerPanel()
    {
        UIManager.Instance.ShowPanelTemplate(PanelScript.PanelType.EditPlayer, playerReference);
    }

    public void OpenReport()
    {
        GameManager.Instance.SetCurrentPlayer(playerReference);
        UIActions.GoToReport();
    }

    public void ChooseEmotions()
    {
        UIManager.Instance.ShowPanelTemplate(PanelScript.PanelType.ChooseEmotions, playerReference);
    }
}
