using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomPart : ToggleCustom
{
    public CharacterCustomization Customization { protected get; set; }

    protected virtual Character.Custom GetCustom(Player player)
    {
        return Character.Custom.FromJson(player.GuideJSON);
    }

    protected override bool IsSameCustomActive(Player player)
    {
        return IsSameCustomActive( GetCustom(player) );
    }

    protected virtual bool IsSameCustomActive(Character.Custom custom) { return false; }
}