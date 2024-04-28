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
    private ThemeColorFilled[] ThemeElements;

    private void Awake()
    {
        LoadPrefabs();
    }

    protected void Start()
    {
        ThemeElements = GetComponentsInChildren<ThemeColorFilled>();
    }

    protected void LoadPrefabs()
    {
        var backgrounds = GameManager.Instance.GetBackgrounds();
        foreach (KeyValuePair<Theme.EBackground, Theme.CustomBackground> bg in backgrounds)
        {
            var instance = Instantiate(themeTogglePrefab, transform);
            instance.GetComponent<Toggle>().group = toggleGroup;
            ToggleTheme themeToggle = instance.GetComponent<ToggleTheme>();
            themeToggle.Background = Background;
            themeToggle.LoadData(bg.Key, bg.Value);
        }
    }

    public void ApplySelection()
    {
        var gm = GameManager.Instance;
        gm.SetCurrentPlayerTheme(Background.Id);

        if (ThemeElements != null && ThemeElements.Length > 0)
        {
            foreach(ThemeColorFilled tcf in ThemeElements)
            {
                if (tcf != null)
                    tcf.LoadPlayerTheme();
            }
        }
    }
}
