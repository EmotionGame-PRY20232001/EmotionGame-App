using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private Texture2D background;
    [SerializeField]
    private bool needsText;
    [SerializeField]
    private GameObject editButton;

    public void ButtonInstantiate(Player player)
    {
        nameText.text = player.Name;
        background = GameManager.Instance.GetBackgrounds()[player.BackgroundId];
        needsText = player.NeedsText;
    }

    public void ActivateEdit()
    {
        if (editButton.activeSelf)
        {
            SceneManager.LoadScene("GameImitate");
        }
        editButton.SetActive(true);
    }
}
