using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeLayout : MonoBehaviour
{
    [SerializeField]
    protected CustomBackground Background;
    [SerializeField]
    private ToggleGroup toggleGroup;
    [SerializeField]
    private GameObject themeTogglePrefab;

    private void Awake()
    {
        LoadPrefabs();
    }

    protected void LoadPrefabs()
    {
        var backgrounds = GameManager.Instance.GetBackgrounds();
        foreach (KeyValuePair<Theme.EBackground, Theme.CustomBackground> bg in backgrounds)
        {
            var instance = Instantiate(themeTogglePrefab, transform);
            instance.GetComponent<Toggle>().group = toggleGroup;
            ThemeToggle themeToggle = instance.GetComponent<ThemeToggle>();
            themeToggle.Background = Background;
            themeToggle.LoadData(bg.Key, bg.Value);
        }
    }

    public void ApplySelection()
    {
        GameManager.Instance.SetCurrentPlayerTheme(Background.Id);
    }
}
