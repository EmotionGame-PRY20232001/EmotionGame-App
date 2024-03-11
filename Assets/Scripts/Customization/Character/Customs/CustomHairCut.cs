using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHairCut : CustomHair
{
    [field: SerializeField]
    public Character.EHairCut HairCut {get; protected set;}
    [SerializeField]
    protected GameObject HairColorsGroup;
    [SerializeField]
    protected CustomHairColor[] HairColors;
    
    public CustomHairCut() {}
    public CustomHairCut(Character.EHairCut hairCut)
    {
        HairCut = hairCut;
    }

    protected override bool IsSameCustomActive(Character.Custom custom)
    {
        SetHairColor(custom.HairColor);
        // Debug.Log("HairCut " + HairCut + " | " + Customization.GetHairCut() );
        // Customization.GetHairCut()
        return HairCut == custom.HairCut;
    }

    protected override void FillHairs()
    {
        if (HairColorsGroup == null) return;
        HairColors = HairColorsGroup.GetComponentsInChildren<CustomHairColor>();
    }

    public void SetHairColor(Character.EHairColor _hairColor)
    {
        GameManager.CustomHair _hair = GameManager.Instance.CharacterCustom.Hairs[_hairColor];

        if (HairEye != null)
            HairEye.color = _hair.Color;

        if (HairBack != null)
            UIutils.SetImage(HairBack, HairCut == Character.EHairCut.None ? null : _hair.HairCuts[HairCut]);
            // HairBack.color = _hair.Color;
    }

    protected override void ChangeSelection()
    {
        Customization.SetHairCut(HairCut);
        foreach (CustomHairColor _hairColor in HairColors)
            _hairColor.SetHairCut(HairCut);
    }
}
