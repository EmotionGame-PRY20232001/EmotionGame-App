using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(ThemeColorFilled))]
[RequireComponent(typeof(Toggle))]
public class ThemeToggleButton : ThemeToggle
{
    [SerializeField]
    protected SpriteToogle ShadowOnOff;

    protected override void CalcTrackThumDiff()
    {
        if (ShadowRect != null && UpRect != null)
            TrackThumbDiff = ShadowRect.sizeDelta.y - UpRect.sizeDelta.y;
    }

    ////////==== State ====////////
    protected override void PlayAnimationSelected(float time)
    {
        ThemeColor?.OnLightnessChange(ThemeToogle.isOn ? Theme.ELightness.Main : Theme.ELightness.Light, time);
        FadeTooltop(false, time);

        OnSetActiveAnimation(ThemeToogle.isOn, time);
    }

    protected override void OnSetActiveAnimation(bool isActive, float time)
    {
        base.OnSetActiveAnimation(isActive, time);

        float moveY = isActive ? -TrackThumbDiff : 0.0f;
        if (ShadowRect != null)
            LeanTween.moveY(ShadowRect, moveY, time);
        if (UpRect != null)
            LeanTween.moveY(UpRect, moveY, time);
    }

    protected override void ToggleIconSprite(bool isActive)
    {
        base.ToggleIconSprite(isActive);
        ShadowOnOff.UpdateToogle(isActive);
    }
}
