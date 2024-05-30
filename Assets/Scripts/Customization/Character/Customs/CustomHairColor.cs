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

    protected override bool IsSameCustomActive(Character.Custom custom)
    {
        SetHairCut(custom.HairCut);
        // Debug.Log("HairColor " + HairColor + " | " + Customization.GetHairColor() );
        // Customization.GetHairColor()
        return HairColor == custom.HairColor;
    }

    protected override void FillHairs()
    {
        if (HairCutsGroup == null) return;
        HairCuts = HairCutsGroup.GetComponentsInChildren<CustomHairCut>();
    }
    
    public void SetHairCut(Character.EHairCut _hairCut)
    {
        if (_hairCut == Character.EHairCut.None)
        {
            Utils.SetImage(HairBack, null);
            return;
        }

        GameManager.CustomHair _hair = GameManager.Instance.CharacterCustom.Hairs[HairColor];

        if (HairBack != null)
            Utils.SetImage(HairBack, _hair.HairCuts[_hairCut]);

        // if (HairBack != null)
        //     HairBack.sprite = GameManager.Instance.CharacterCustom.HairCuts[_hairCut];
    }

    protected override void ChangeSelection()
    {
        Customization?.SetHairColor(HairColor);
        if (HairCuts != null)
        {
            foreach (CustomHairCut _hairCut in HairCuts)
            {
                if (_hairCut != null)
                    _hairCut.SetHairColor(HairColor);
            }
        }
    }
}