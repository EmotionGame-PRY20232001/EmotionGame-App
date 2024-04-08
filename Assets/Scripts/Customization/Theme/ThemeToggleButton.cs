using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(ThemeColorFilled))]
[RequireComponent(typeof(Toggle))]
public class ThemeToggleButton : ThemeButton
{
    protected Toggle ThemeToogle;
    protected float ShadowUpHeightDiff;

    [SerializeField]
    protected SpriteToogle IconOnOff;
    [SerializeField]
    protected SpriteToogle ShadowOnOff;

    ////////==== Unity ====////////
    protected override void Awake()
    {
        base.Awake();
        ThemeToogle = gameObject.GetComponent<Toggle>();
    }

    protected override void Start()
    {
        if (ShadowRect != null && UpRect != null)
            ShadowUpHeightDiff = ShadowRect.sizeDelta.y - UpRect.sizeDelta.y;

        base.Start();
    }

    ////////==== State ====////////
    protected override void PlayAnimationNormal(float time)
    {
        ThemeColor?.OnLightnessChange(Theme.ELightness.Main, time);
        FadeTooltop(false, time);

        FadeShadowHeight(ThemeToogle.isOn, time);
    }
    protected override void PlayAnimationHighlighted(float time)
    {
    }
    protected override void PlayAnimationPressed(float time)
    {
        ThemeColor?.OnLightnessChange(Theme.ELightness.Dark, time);
        FadeTooltop(true, time);

        FadeShadowHeight(!ThemeToogle.isOn, time);
    }
    protected override void PlayAnimationSelected(float time)
    {
        ThemeColor?.OnLightnessChange(ThemeToogle.isOn ? Theme.ELightness.Main : Theme.ELightness.Light, time);
        FadeTooltop(false, time);

        FadeShadowHeight(ThemeToogle.isOn, time);
    }

    protected override void FadeShadowHeight(bool down, float time)
    {
        ToggleIconSprite(!down);

        float moveY = down ? -ShadowUpHeightDiff : 0.0f;
        if (ShadowRect != null)
            LeanTween.moveY(ShadowRect, moveY, time);
        if (UpRect != null)
            LeanTween.moveY(UpRect, moveY, time);
    }

    protected void ToggleIconSprite(bool up)
    {
        //ThemeToogle.isOn
        ShadowOnOff.UpdateToogle(up);
        IconOnOff.UpdateToogle(up);
    }

    [System.Serializable]
    public struct SpriteToogle
    {
        public Image ImageT;
        public Sprite SpriteOn;
        public Sprite SpriteOff;

        public void UpdateToogle(bool isOn)
        {
            if (ImageT == null) return;

            if (isOn)
            {
                if (SpriteOn != null)
                    ImageT.sprite = SpriteOn;
            }
            else
            {
                if (SpriteOff != null)
                    ImageT.sprite = SpriteOff;
            }
        }
    }
}
