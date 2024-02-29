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
    private RawImage background;
    [SerializeField]
    private Image guide;
    [SerializeField]
    private bool needsText;
    [SerializeField]
    private GameObject editButton;

    private Player playerReference;

    public void ButtonInstantiate(Player player)
    {
        playerReference = player;
        nameText.text = player.Name;
        background.texture = GameManager.Instance.GetBackgrounds()[player.BackgroundId];
        guide.sprite = GameManager.Instance.GetGuideSprites()[player.GuideId];
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

    public void OpenEditPlayerPanel()
    {
        UIManager.Instance.ShowPanelTemplate("Edit Player", playerReference);
    }
}
