using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomHair : CustomPart
{
    [SerializeField]
    protected Image HairBack;
    [SerializeField]
    protected Image HairEye;

    protected virtual void Awake()
    {
        FillHairs();
    }

    protected virtual void FillHairs() { }
}
