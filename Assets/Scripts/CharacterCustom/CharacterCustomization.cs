using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Testing OnSelectionChange
// using UnityEditor;
// using UnityEngine.UIElements;

public class CharacterCustomization : MonoBehaviour
{
    // public Dictionary<Character.ESkinColor, Sprite> SkinColors = new Dictionary<Character.ESkinColor, Sprite>();
    [SerializeField]
    private List<UIutils.CustomSprites<Character.ESkinColor>> SkinColors;
    [SerializeField]
    private List<UIutils.CustomSpritesMul<Character.EHairCut, Character.EHairColor>> Hairs;
    [SerializeField]
    private List<UIutils.CustomSprites<Character.EHairColor>> Eyebrows;
    [SerializeField]
    private List<UIutils.CustomSprites<Character.EHairColor>> Eyelashes;
    [SerializeField]
    private List<UIutils.CustomSprites<Character.EEyeColor>> EyeColors;
    [SerializeField]
    private List<UIutils.CustomSprites<Character.EShirt>> Shirts;

    [SerializeField]
    private Character.GOParts Parts;
    
    protected Character.Custom Customization;
    
    public void SetHairCut(CustomHairCut hairCut)
    {
        Customization.HairCut = hairCut.HairCut;
        UIutils.SetImage(Parts.HairBack,
                         Hairs.Find( x => x.Key1 == hairCut.HairCut && x.Key2 == Customization.HairColor ).Value);
    }
    public void SetHairColor(CustomHairColor hairColor)
    {
        Customization.HairColor = hairColor.HairColor;
        UIutils.SetImage(Parts.HairBack,
                         Hairs.Find( x => x.Key1 == Customization.HairCut && x.Key2 == hairColor.HairColor ).Value);
        Sprite eyelashes = Eyelashes.Find( x => x.Key == hairColor.HairColor ).Value;
        UIutils.SetImage(Parts.EyelashesL, eyelashes);
        UIutils.SetImage(Parts.EyelashesR, eyelashes);
        Sprite eyebrows = Eyebrows.Find( x => x.Key == hairColor.HairColor ).Value;
        UIutils.SetImage(Parts.EyebrowL, eyebrows);
        UIutils.SetImage(Parts.EyebrowR, eyebrows);
    }
    public void SetEyeColor(CustomEyeColor eyeColor)
    {
        Customization.EyeColor = eyeColor.EyeColor;
        Sprite iris = EyeColors.Find( x => x.Key == eyeColor.EyeColor ).Value;
        UIutils.SetImage(Parts.IrisL, iris);
        UIutils.SetImage(Parts.IrisR, iris);
    }
    public void SetSkinColor(CustomSkinColor skinColor)
    {
        Customization.SkinColor = skinColor.SkinColor;
        UIutils.SetImage(Parts.Body, SkinColors.Find( x => x.Key == skinColor.SkinColor ).Value);
    }
    public void SetShirt(CustomShirt shirt)
    {
        Customization.Shirt = shirt.Shirt;
        UIutils.SetImage(Parts.Shirt, Shirts.Find( x => x.Key == shirt.Shirt ).Value);
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
        UIutils.SetImage(Parts.HairBack, Hairs.Find( x => x.Key1 == hairCut && x.Key2 == Customization.HairColor ).Value);
    }
    protected void _SetHairColor(Character.EHairColor hairColor)
    {
        Customization.HairColor = hairColor;
        UIutils.SetImage(Parts.HairBack, Hairs.Find( x => x.Key1 == Customization.HairCut && x.Key2 == hairColor ).Value);
        Sprite eyelashes = Eyelashes.Find( x => x.Key == hairColor ).Value;
        UIutils.SetImage(Parts.EyelashesL, eyelashes);
        UIutils.SetImage(Parts.EyelashesR, eyelashes);
        Sprite eyebrows = Eyebrows.Find( x => x.Key == hairColor ).Value;
        UIutils.SetImage(Parts.EyebrowL, eyebrows);
        UIutils.SetImage(Parts.EyebrowR, eyebrows);
    }
    protected void _SetEyeColor(Character.EEyeColor eyeColor)
    {
        Customization.EyeColor = eyeColor;
        Sprite iris = EyeColors.Find( x => x.Key == eyeColor ).Value;
        UIutils.SetImage(Parts.IrisL, iris);
        UIutils.SetImage(Parts.IrisR, iris);
    }
    protected void _SetSkinColor(Character.ESkinColor skinColor)
    {
        Customization.SkinColor = skinColor;
        UIutils.SetImage(Parts.Body, SkinColors.Find( x => x.Key == skinColor ).Value);
    }
    protected void _SetShirt(Character.EShirt shirt)
    {
        Customization.Shirt = shirt;
        UIutils.SetImage(Parts.Shirt, Shirts.Find( x => x.Key == shirt ).Value);
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
