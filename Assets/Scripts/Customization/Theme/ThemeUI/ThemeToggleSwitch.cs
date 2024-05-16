using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(ThemeColorFilled))]
[RequireComponent(typeof(Toggle))]
public class ThemeToggleSwitch : ThemeToggle
{
    [SerializeField]
    protected TrackImage IconTrack;

    protected override void CalcTrackThumDiff()
    {
        if (ShadowRect != null && UpRect != null)
            TrackThumbDiff = ShadowRect.sizeDelta.x - UpRect.sizeDelta.x;
        IconTrack.MoveDist = TrackThumbDiff - (UpRect.sizeDelta.x / 2);
    }

    protected override void PlaySfx(bool isOn)
    {
        SfxType = isOn ? AudioSrcs.ESfxButton.On : AudioSrcs.ESfxButton.Off;
        LoadSfx();
        AudioSrc?.Play();
    }

    ////////==== State ====////////
    protected override void PlayAnimationSelected(float time)
    {
        ThemeColor?.OnLightnessChange(Theme.ELightness.Main, time);
        base.PlayAnimationSelected(time);
    }

    // Will move Toogle Left or Right
    // < OFF | ON >
    protected override void OnSetActiveAnimation(bool isActive, float time)
    {
        base.OnSetActiveAnimation(isActive, time);

        float moveX = isActive ? TrackThumbDiff : 0.0f;

        //Track = Shadow | Thumb = Up
        if (UpRect != null)
        {
            LeanTween.moveX(UpRect, moveX, time);
            moveX = isActive ? IconTrack.MoveDist : -IconTrack.MoveDist;
        }

        if (IconTrack.Rect != null)
            LeanTween.moveX(IconTrack.Rect, -moveX, time);
    }

    protected override void ToggleIconSprite(bool isActive)
    {
        base.ToggleIconSprite(isActive);
        if (IconTrack.Img)
        {
            Image aux = IconThumb.ImageT;
            IconThumb.ImageT = IconTrack.Img;
            IconThumb.UpdateToogle(!isActive);
            IconThumb.ImageT = aux;
        }
    }


    ////////==== Structs ====////////
    [System.Serializable]
    public struct TrackImage
    {
        public Image Img;
        public RectTransform Rect;
        public float MoveDist;
    }
}
