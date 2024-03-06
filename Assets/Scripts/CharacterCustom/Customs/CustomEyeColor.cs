using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEyeColor : MonoBehaviour
{
    [field: SerializeField]
    public Character.EEyeColor EyeColor {get; private set;}
    private Toggle toggle;
    public CharacterCustomization Customization;

    public CustomEyeColor() {}
    public CustomEyeColor(Character.EEyeColor eyeColor)
    {
        EyeColor = eyeColor;
    }
}