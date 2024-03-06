using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class CustomSkinColor : MonoBehaviour
{
    [field: SerializeField]
    public Character.ESkinColor SkinColor {get; private set;}
    [SerializeField]
    private Toggle toggle;
    public CharacterCustomization Customization;

    public CustomSkinColor() {}
    public CustomSkinColor(Character.ESkinColor skinColor)
    {
        SkinColor = skinColor;
    }

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            Customization._SetSkinColor(SkinColor);
        });
    }
}