using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    /// ENUMS
    public enum ESkinColor { Lightest, Light, Medium, Dark, Deep }
    public enum EEyeColor { Dark, Brown, Green, Blue, Gray }
    public enum EHairColor { Black, Brown, Redhead, Blonde, Albino }
    public enum EHairCut { None, WeavyShort, Medium, Long }
    public enum EShirt { Calm, Sport, Andean, Forest, Plaid, }


    /// STRUCTS

    [System.Serializable]
    public struct Custom
    {
        public ESkinColor SkinColor;
        public EEyeColor EyeColor;
        public EHairColor HairColor;
        public EHairCut HairCut;
        public EShirt Shirt;

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
        public static Custom FromJson(string jsonString)
        {
            if (jsonString == "") return GetDefault();
            return JsonUtility.FromJson<Custom>(jsonString);
        }

        public static Custom GetDefault()
        {
            var custom = new Custom();
            custom.SkinColor   = ESkinColor.Medium;
            custom.EyeColor    = EEyeColor.Dark;
            custom.HairColor   = EHairColor.Black;
            custom.HairCut     = EHairCut.Medium;
            custom.Shirt       = EShirt.Calm;
            return custom;
        }
    }

    [System.Serializable]
    public class GOParts
    {
        public GameObject Body;
        public GameObject HairBack;
        public SymetricPart Eyelashes;
        public SymetricPart Eyebrow;
        public SymetricPart Sclera;
        public SymetricPart Iris;
        public GameObject Mouth;
        public GameObject Shirt;
    }

    [System.Serializable]
    public class SymetricPart
    {
        public GameObject Left;
        public GameObject Right;
    }

    /// CUSTOMIZATION

    [field: SerializeField]
    protected Custom Customization;
    [SerializeField]
    private GOParts Parts;

    protected void SetHair(EHairCut hairCut, EHairColor hairColor)
    {
        Customization.HairCut = hairCut;
        Customization.HairColor = hairColor;
        SetHairCut(hairCut);
        SetHairColor(hairColor);
    }

    public EHairCut GetHairCut() { return Customization.HairCut; }
    public void SetHairCut(EHairCut hairCut)
    {
        Customization.HairCut = hairCut;
        Sprite _hairCut = hairCut == EHairCut.None ? null :
                                        GameManager.Instance.CharacterCustom.Hairs[Customization.HairColor].HairCuts[hairCut];
        UIutils.SetImage(Parts.HairBack, _hairCut);
    }

    public EHairColor GetHairColor() { return Customization.HairColor; }
    public void SetHairColor(EHairColor hairColor)
    {
        Customization.HairColor = hairColor;

        if (Customization.HairCut != EHairCut.None)
        {
            Sprite hairBack = GameManager.Instance.CharacterCustom.Hairs[hairColor].HairCuts[Customization.HairCut];
            UIutils.SetImage(Parts.HairBack, hairBack);
        }

        Sprite _eyelashes = GameManager.Instance.CharacterCustom.Hairs[hairColor].Eyelashes;
        UIutils.SetImage(Parts.Eyelashes.Left, _eyelashes);
        UIutils.SetImage(Parts.Eyelashes.Right, _eyelashes);

        Sprite _eyebrows = GameManager.Instance.CharacterCustom.Hairs[hairColor].Eyebrow;
        UIutils.SetImage(Parts.Eyebrow.Left, _eyebrows);
        UIutils.SetImage(Parts.Eyebrow.Right, _eyebrows);
    }

    public EEyeColor GetEyeColor() { return Customization.EyeColor; }
    public void SetEyeColor(EEyeColor eyeColor)
    {
        Customization.EyeColor = eyeColor;
        Sprite _iris = GameManager.Instance.CharacterCustom.EyeColors[eyeColor];
        UIutils.SetImage(Parts.Iris.Left, _iris);
        UIutils.SetImage(Parts.Iris.Right, _iris);
    }

    public ESkinColor GetSkinColor() { return Customization.SkinColor; }
    public void SetSkinColor(ESkinColor skinColor)
    {
        Customization.SkinColor = skinColor;
        Sprite _skin = GameManager.Instance.CharacterCustom.SkinColors[skinColor];
        UIutils.SetImage(Parts.Body, _skin);
    }

    public EShirt GetShirt() { return Customization.Shirt; }
    public void SetShirt(EShirt shirt)
    {
        Customization.Shirt = shirt;
        Sprite _shirt = GameManager.Instance.CharacterCustom.Shirts[shirt];
        UIutils.SetImage(Parts.Shirt, _shirt);
    }

    public string GetJSON()
    {
        return Customization.ToJson();
    }
    public void SetByJson(string jsonCustom)
    {
        Debug.Log("SetByJson " + jsonCustom);
        ChangeCustomization(Custom.FromJson(jsonCustom));
    }

    public void ChangeCustomization(Custom _customization)
    {
        Customization = _customization;
        SetHair(Customization.HairCut, Customization.HairColor);
        SetEyeColor(Customization.EyeColor);
        SetSkinColor(Customization.SkinColor);
        SetShirt(Customization.Shirt);
    }
}