using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSkinColor : CustomPart
{
    [field: SerializeField]
    public Character.ESkinColor SkinColor {get; private set;}
    
    public CustomSkinColor() {}
    public CustomSkinColor(Character.ESkinColor skinColor)
    {
        SkinColor = skinColor;
    }
    
    protected override bool IsSameCustomActive(Character.Custom custom)
    {
        // Debug.Log("SkinColor " + SkinColor + " | " + Customization.GetSkinColor() );
        // Customization.GetSkinColor()
        return SkinColor == custom.SkinColor;
    }

    protected override void ChangeSelection()
    {
        Customization.SetSkinColor(SkinColor);
    }
}