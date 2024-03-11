using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEyeColor : CustomPart
{
    [field:SerializeField]
    public Character.EEyeColor EyeColor {get; private set;}
    
    public CustomEyeColor() {}
    public CustomEyeColor(Character.EEyeColor eyeColor)
    {
        EyeColor = eyeColor;
    }
    
    protected override bool IsSameCustomActive(Character.Custom custom)
    {
        // Debug.Log("EyeColor " + EyeColor + " | " + Customization.GetEyeColor() );
        // Customization.GetEyeColor()
        return EyeColor == custom.EyeColor;
    }

    protected override void ChangeSelection()
    {
        Customization.SetEyeColor(EyeColor);
    }
}