using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHairColor : CustomHair
{
    [field: SerializeField]
    public Character.EHairColor HairColor {get; protected set;}
    [SerializeField]
    protected GameObject HairCutsGroup;
    [SerializeField]
    protected CustomHairCut[] HairCuts;
    
    public CustomHairColor() {}
    public CustomHairColor(Character.EHairColor hairColor)
    {
        HairColor = hairColor;
    }
    
    protected override void Start()
    {
        base.Start();
        toggle.isOn = HairColor == Customization.GetHairColor();
        FillHairCuts();
        SetHairCut(Customization.GetHairCut());
    }

    protected override void SetPart()
    {
        Customization.SetHairColor(HairColor);
        foreach (CustomHairCut _hairCut in HairCuts)
            _hairCut.SetColor(HairColor);
    }

    public void FillHairCuts()
    {
        if (HairCutsGroup == null) return;
        HairCuts = HairCutsGroup.GetComponentsInChildren<CustomHairCut>();
    }
    
    public void SetHairCut(Character.EHairCut _hairCut)
    {
        if (_hairCut == Character.EHairCut.None)
        {
            UIutils.SetImage(HairBack, null);
            return;
        }

        GameManager.CustomHair _hair = GameManager.Instance.CharacterCustom.Hairs[HairColor];

        if (HairBack != null)
            UIutils.SetImage(HairBack, _hair.HairCuts[_hairCut]);

        // if (HairBack != null)
        //     HairBack.sprite = GameManager.Instance.CharacterCustom.HairCuts[_hairCut];
    }
}