using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTheme : ToggleCustom
{
    public ThemeLayout Layout { protected get; set; }
    
    [field:SerializeField]
    public Theme.EBackground Theme { get; private set; }

    public ToggleTheme() {}
    public ToggleTheme(Theme.EBackground _theme)
    {
        Theme = _theme;
    }

    protected override bool IsSameCustomActive(Player player)
    {
        // Background.Id
        return Theme == (Theme.EBackground)player.BackgroundId;
    }

    protected override void ChangeSelection()
    {
        Layout.ChangeSelection(Theme);
    }
    
    
    [SerializeField]
    private Image thumbnail;
    [SerializeField]
    private TMP_Text txtName;

    public void LoadData(Theme.EBackground bg, Theme.CustomBackground data)
    {
        Theme = bg;
        thumbnail.sprite = data.Texture;
        txtName.text = data.Name;
    }
}
