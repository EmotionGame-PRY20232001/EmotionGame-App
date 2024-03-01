using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHairCut : MonoBehaviour
{
    [field: SerializeField]
    public Character.EHairCut HairCut {get; private set;}

    public CustomHairCut() {}
    public CustomHairCut(Character.EHairCut hairCut)
    {
        HairCut = hairCut;
    }
}
