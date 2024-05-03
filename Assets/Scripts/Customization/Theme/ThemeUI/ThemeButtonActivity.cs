using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeButtonActivity : ThemeButton
{
    [SerializeField]
    protected float ShadowHeight = 16.0f;

    protected override void OnSetActiveAnimation(bool isActive, float time)
    {
        if (UpRect == null) return;
        if (isActive)
        {
            LeanTween.moveY(UpRect, 0.0f, time);
        }
        else
        {
            LeanTween.moveY(UpRect, ShadowHeight, time);
        }
    }
}
