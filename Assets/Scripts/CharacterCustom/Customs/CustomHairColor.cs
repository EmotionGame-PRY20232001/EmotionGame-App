using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHairColor : MonoBehaviour
{
    [field: SerializeField]
    public Character.EHairColor HairColor {get; private set;}

    public CustomHairColor() {}
    public CustomHairColor(Character.EHairColor hairColor)
    {
        HairColor = hairColor;
    }
}