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
    
    protected override void Start()
    {
        toggle.isOn = SkinColor == Customization.GetSkinColor();
    }

    protected override void SetPart()
    {
        Customization.SetSkinColor(SkinColor);
    }
}