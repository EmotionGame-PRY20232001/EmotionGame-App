using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSkinColor : MonoBehaviour
{
    [field: SerializeField]
    public Character.ESkinColor SkinColor {get; private set;}
    
    public CustomSkinColor() {}
    public CustomSkinColor(Character.ESkinColor skinColor)
    {
        SkinColor = skinColor;
    }
}