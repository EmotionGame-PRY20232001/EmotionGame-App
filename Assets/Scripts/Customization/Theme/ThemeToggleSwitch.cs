using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(ThemeColorFilled))]
[RequireComponent(typeof(Toggle))]
public class ThemeToggleSwitch : ThemeButton
{
    protected Toggle ThemeToogle;
    protected float TrackThumbDiff = 0.0f;

    [SerializeField]
    protected SpriteToogle IconThumb;
    [SerializeField]
    protected TrackImage IconTrack;

    ////////==== Unity ====////////
    protected override void Awake()
    {
        base.Awake();
        ThemeToogle = gameObject.GetComponent<Toggle>();
    }

    protected override void Start()
    {
        CalcTrackThumDiff();
        base.Start();
    }

    protected virtual void CalcTrackThumDiff()
    {
        if (ShadowRect != null && UpRect != null)
            TrackThumbDiff = ShadowRect.sizeDelta.x - UpRect.sizeDelta.x;
    }

    ////////==== State ====////////
    protected override void PlayAnimationNormal(float time)
    {
        ThemeColor?.OnLightnessChange(Theme.ELightness.Main, time);
        FadeTooltop(false, time);

        OnSetActiveAnimation(ThemeToogle.isOn, time);
    }
    protected override void PlayAnimationHighlighted(float time)
    {
    }
    protected override void PlayAnimationPressed(float time)
    {
        ThemeColor?.OnLightnessChange(Theme.ELightness.Dark, time);
        FadeTooltop(true, time);

        OnSetActiveAnimation(!ThemeToogle.isOn, time);
    }
    protected override void PlayAnimationSelected(float time)
    {
        ThemeColor?.OnLightnessChange(Theme.ELightness.Main, time);
        FadeTooltop(false, time);

        OnSetActiveAnimation(ThemeToogle.isOn, time);
    }

    // Will move Toogle Left or Right
    // < OFF | ON >
    protected override void OnSetActiveAnimation(bool isActive, float time)
    {
        float moveX = isActive ? TrackThumbDiff : 0.0f;

        //Track = Shadow | Thumb = Up
        if (UpRect != null)
            LeanTween.moveX(UpRect, moveX, time).setOnComplete( ToggleIconSprite );

        if (IconTrack.Rect != null)
            LeanTween.moveX(IconTrack.Rect, -moveX, time);
    }

    protected virtual void ToggleIconSprite()
    {
        IconThumb.UpdateToogle(ThemeToogle.isOn);

        if (IconTrack.Img)
        {
            Image aux = IconThumb.ImageT;
            IconThumb.ImageT = IconTrack.Img;
            IconThumb.UpdateToogle(!ThemeToogle.isOn);
            IconThumb.ImageT = aux;
        }
    }

    ////////==== Structs ====////////
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

    [System.Serializable]
    public struct TrackImage
    {
        public Image Img;
        public RectTransform Rect;
    }
}
