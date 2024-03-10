using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEyeColor : CustomPart
{
    [field: SerializeField]
    public Character.EEyeColor EyeColor {get; private set;}
    
    public CustomEyeColor() {}
    public CustomEyeColor(Character.EEyeColor eyeColor)
    {
        EyeColor = eyeColor;
    }
    
    protected override void Start()
    {
        toggle.isOn = EyeColor == Customization.GetEyeColor();
    }

    protected override void SetPart()
    {
        Customization.SetEyeColor(EyeColor);
    }
}