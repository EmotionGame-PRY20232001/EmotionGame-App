using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeToggle : MonoBehaviour
{
    [SerializeField]
    private int themeId;
    [SerializeField]
    private RawImage themeImage;
    [SerializeField]
    private TMP_Text themeName;
    [SerializeField]
    private ThemeLayout themeLayout;

    public void LoadData(int backgroundId, Texture2D background, string backgroundName)
    {
        themeId = backgroundId;
        themeImage.texture = background;
        themeName.text = backgroundName;
    }

    public void ChangeSelection()
    {
        GameManager.Instance.ChangeBackground(themeId); 
        themeLayout = GetComponentInParent<ThemeLayout>();
        themeLayout.SetSelectedTheme(themeId);
    }

    public int GetThemeId()
    {
        return themeId;
    }
}
