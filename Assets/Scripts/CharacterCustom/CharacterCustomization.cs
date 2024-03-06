using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Testing OnSelectionChange
// using UnityEditor;
// using UnityEngine.UIElements;

public class CharacterCustomization : MonoBehaviour
{
    [SerializeField]
    private Character.GOParts Parts;
    
    protected Character.Custom Customization;
    
    public void SetHairCut(CustomHairCut hairCut)
    {
        _SetHairCut(hairCut.HairCut);
    }
    public void SetHairColor(CustomHairColor hairColor)
    {
        _SetHairColor(hairColor.HairColor);
    }
    public void SetEyeColor(CustomEyeColor eyeColor)
    {
        _SetEyeColor(eyeColor.EyeColor);
    }
    public void SetSkinColor(CustomSkinColor skinColor)
    {
        _SetSkinColor(skinColor.SkinColor);
    }
    public void SetShirt(CustomShirt shirt)
    {
        _SetShirt(shirt.Shirt);
    }
    protected void _SetHair(Character.EHairCut hairCut, Character.EHairColor hairColor)
    {
        Customization.HairCut = hairCut;
        Customization.HairColor = hairColor;
        _SetHairCut(hairCut);
        _SetHairColor(hairColor);
    }
    protected void _SetHairCut(Character.EHairCut hairCut)
    {
        Customization.HairCut = hairCut;
        Sprite _hairCut = GameManager.Instance.Hairs.Find( x => x.Key1 == hairCut &&
                                                                x.Key2 == Customization.HairColor ).Value;
        UIutils.SetImage(Parts.HairBack, _hairCut);
    }
    protected void _SetHairColor(Character.EHairColor hairColor)
    {
        Customization.HairColor = hairColor;
        Sprite hairBack = GameManager.Instance.Hairs.Find( x => x.Key1 == Customization.HairCut &&
                                                                x.Key2 == hairColor ).Value;
        UIutils.SetImage(Parts.HairBack, hairBack);
        Sprite _eyelashes = GameManager.Instance.Eyelashes.Find( x => x.Key == hairColor ).Value;
        UIutils.SetImage(Parts.EyelashesL, _eyelashes);
        UIutils.SetImage(Parts.EyelashesR, _eyelashes);
        Sprite _eyebrows = GameManager.Instance.Eyebrows.Find( x => x.Key == hairColor ).Value;
        UIutils.SetImage(Parts.EyebrowL, _eyebrows);
        UIutils.SetImage(Parts.EyebrowR, _eyebrows);
    }
    protected void _SetEyeColor(Character.EEyeColor eyeColor)
    {
        Customization.EyeColor = eyeColor;
        Sprite _iris = GameManager.Instance.EyeColors.Find( x => x.Key == eyeColor ).Value;
        UIutils.SetImage(Parts.IrisL, _iris);
        UIutils.SetImage(Parts.IrisR, _iris);
    }
    public void _SetSkinColor(Character.ESkinColor skinColor)
    {
        Customization.SkinColor = skinColor;
        Sprite _skin = GameManager.Instance.SkinColors.Find( x => x.Key == skinColor ).Value;
        UIutils.SetImage(Parts.Body, _skin);
    }
    protected void _SetShirt(Character.EShirt shirt)
    {
        Customization.Shirt = shirt;
        Sprite _shirt = GameManager.Instance.Shirts.Find( x => x.Key == shirt ).Value;
        UIutils.SetImage(Parts.Shirt, _shirt);
    }

    // Start is called before the first frame update
    void Start()
    {
        _SetHair(Character.EHairCut.Medium, Character.EHairColor.Black);
        _SetEyeColor(Character.EEyeColor.Dark);
        _SetSkinColor(Character.ESkinColor.Medium);
        _SetShirt(Character.EShirt.Calm);
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
