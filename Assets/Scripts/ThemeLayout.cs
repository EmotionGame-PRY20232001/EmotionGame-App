using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeLayout : MonoBehaviour
{
    [SerializeField]
    private ToggleGroup toggleGroup;
    [SerializeField]
    private GameObject themeTogglePrefab;
    [SerializeField]
    private int selectedTheme;

    private void Awake()
    {
        var backgrounds = GameManager.Instance.GetBackgrounds();
        var backgroundNames = GameManager.Instance.GetBackgroundNames();
        var player = GameManager.Instance.GetCurrentPlayer();
        selectedTheme = player.BackgroundId;
        for (int i = 0; i < backgrounds.Count; i++)
        {
            var instance = Instantiate(themeTogglePrefab, transform);
            var toggle = instance.GetComponent<Toggle>();
            toggle.isOn = (player.BackgroundId == i);
            toggle.group = toggleGroup;
            instance.GetComponent<ThemeToggle>().LoadData(i, backgrounds[i], backgroundNames[i]);
        }
    }

    public void ApplySelection()
    {
        var player = GameManager.Instance.GetCurrentPlayer();
        player.BackgroundId = selectedTheme;
        GameManager.Instance.SetCurrentPlayer(player);
    }

    public void SetSelectedTheme(int themeId)
    {
        selectedTheme = themeId;
    }
}
