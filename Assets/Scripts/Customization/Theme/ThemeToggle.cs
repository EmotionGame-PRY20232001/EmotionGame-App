using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(ThemeColorFilled))]
[RequireComponent(typeof(Toggle))]
//public class ThemeButton : Button
//{

//protected override void Awake()
//{
//base.Awake();
//}

//    protected override void Start()
//    {
//        base.Start();
//    }

//    protected override void DoStateTransition(SelectionState state, bool instant)
//    {
//        base.DoStateTransition(state, instant);
//PrevState = state;
//switch (state)
//{
//    case SelectionState.Normal:
//        if (UpRect != null)
//            //AnimateShadowToUpSize(DefaultShadowSize);
//        break;
//    case SelectionState.Highlighted:
//        if (UpRect != null)
//            //AnimateShadowToUpSize(HighlightedShadowSize);
//        break;
//    case SelectionState.Pressed:
//        if (UpRect != null)
//            //AnimateShadowToUpSize(UpRect.sizeDelta);
//        break;
//    case SelectionState.Selected:
//        if (UpRect != null)
//            //AnimateShadowToUpSize(UpRect.sizeDelta);
//        break;
//    case SelectionState.Disabled:
//        break;
//}
//        
//    }

//    //protected void AnimateShadowToUpSize(Vector2 newSize)
//    //{
//    //    if (ShadowRect != null)
//    //    {
//    //        LeanTween.size(ShadowRect, newSize, TransitionTime);
//    //    }
//    //}
//}


public class ThemeToggle : ThemeButton
{
    protected Toggle ThemeToogle;
    protected float TrackThumbDiff = 0.0f;
    [SerializeField]
    protected SpriteToogle IconThumb;

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
    protected virtual void CalcTrackThumDiff() { }


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

    protected override void OnSetActiveAnimation(bool isActive, float time)
    {
        ToggleIconSprite(isActive);
    }
    protected virtual void ToggleIconSprite(bool isActive)
    {
        IconThumb.UpdateToogle(isActive);
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
}
