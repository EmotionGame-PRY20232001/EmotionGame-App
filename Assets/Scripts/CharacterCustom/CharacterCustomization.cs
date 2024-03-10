using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Testing OnSelectionChange
// using UnityEditor;
// using UnityEngine.UIElements;

public class CharacterCustomization : MonoBehaviour
{
    [SerializeField]
    protected Character.Custom Customization;
    [SerializeField]
    private Character.GOParts Parts;
    
    protected void SetHair(Character.EHairCut hairCut, Character.EHairColor hairColor)
    {
        Customization.HairCut = hairCut;
        Customization.HairColor = hairColor;
        SetHairCut(hairCut);
        SetHairColor(hairColor);
    }
    
    public Character.EHairCut GetHairCut() { return Customization.HairCut; }
    public void SetHairCut(Character.EHairCut hairCut)
    {
        Customization.HairCut = hairCut;
        Sprite _hairCut = hairCut == Character.EHairCut.None ? null :
                                        GameManager.Instance.CharacterCustom.Hairs[Customization.HairColor].HairCuts[hairCut];
        UIutils.SetImage(Parts.HairBack, _hairCut);
    }

    public Character.EHairColor GetHairColor() { return Customization.HairColor; }
    public void SetHairColor(Character.EHairColor hairColor)
    {
        Customization.HairColor = hairColor;
        
        if (Customization.HairCut != Character.EHairCut.None)
        {
            Sprite hairBack = GameManager.Instance.CharacterCustom.Hairs[hairColor].HairCuts[Customization.HairCut];
            UIutils.SetImage(Parts.HairBack, hairBack);
        }

        Sprite _eyelashes = GameManager.Instance.CharacterCustom.Hairs[hairColor].Eyelashes;
        UIutils.SetImage(Parts.EyelashesL, _eyelashes);
        UIutils.SetImage(Parts.EyelashesR, _eyelashes);
        
        Sprite _eyebrows = GameManager.Instance.CharacterCustom.Hairs[hairColor].Eyebrow;
        UIutils.SetImage(Parts.EyebrowL, _eyebrows);
        UIutils.SetImage(Parts.EyebrowR, _eyebrows);
    }
    
    public Character.EEyeColor GetEyeColor() { return Customization.EyeColor; }
    public void SetEyeColor(Character.EEyeColor eyeColor)
    {
        Customization.EyeColor = eyeColor;
        Sprite _iris = GameManager.Instance.CharacterCustom.EyeColors[eyeColor];
        UIutils.SetImage(Parts.IrisL, _iris);
        UIutils.SetImage(Parts.IrisR, _iris);
    }

    public Character.ESkinColor GetSkinColor() { return Customization.SkinColor; }
    public void SetSkinColor(Character.ESkinColor skinColor)
    {
        Customization.SkinColor = skinColor;
        Sprite _skin = GameManager.Instance.CharacterCustom.SkinColors[skinColor];
        UIutils.SetImage(Parts.Body, _skin);
    }

    public Character.EShirt GetShirt() { return Customization.Shirt; }
    public void SetShirt(Character.EShirt shirt)
    {
        Customization.Shirt = shirt;
        Sprite _shirt = GameManager.Instance.CharacterCustom.Shirts[shirt];
        UIutils.SetImage(Parts.Shirt, _shirt);
    }
    
    void Start()
    {
        SetHair(Customization.HairCut, Customization.HairColor);
        SetEyeColor(Customization.EyeColor);
        SetSkinColor(Customization.SkinColor);
        SetShirt(Customization.Shirt);
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
