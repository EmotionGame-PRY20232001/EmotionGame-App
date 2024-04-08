using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
[RequireComponent(typeof(ThemeColorFilled))]
[RequireComponent(typeof(Button))]
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


public class ThemeButton : MonoBehaviour
{
    protected Button Btn;

    //---- State ----//
    protected SelectionState CurrentState = SelectionState.Normal;
    public SelectionState State
    {
        get { return CurrentState; }
        set { ChangeState(value, false); }
    }

    //---- Tooltip ----//
    protected ThemeColorFilled ThemeColor;
    [SerializeField]
    protected float TransitionTime = 0.15f;
    [SerializeField]
    protected float AlphaOnDisabled = 0.5f;

    //---- Transform ----//
    [SerializeField]
    protected RectTransform ShadowRect;
    [SerializeField]
    protected RectTransform UpRect;
    protected Vector2 DefaultShadowSize;

    //---- Tooltip ----//
    [SerializeField]
    protected CanvasGroup Tooltip;

    ////////==== Unity ====////////
    protected void Awake()
    {
        ThemeColor = gameObject.GetComponent<ThemeColorFilled>();
        Btn = gameObject.GetComponent<Button>();
    }

    protected void Start()
    {
        if (ShadowRect != null)
        {
            DefaultShadowSize = ShadowRect.sizeDelta;
        }

        ChangeState(SelectionState.Normal, true);
    }

    ////////==== State ====////////
    protected void ChangeState(SelectionState state, bool instant)
    {
        CurrentState = state;
        float time = instant ? 0.0f : TransitionTime;

        switch (state)
        {
            case SelectionState.Normal:
                ThemeColor?.OnLightnessChange(Theme.ELightness.Main, time);
                FadeTooltop(false, time);
                FadeShadowHeight(false, time);
                break;
            case SelectionState.Highlighted:
                ThemeColor?.OnLightnessChange(Theme.ELightness.Light, time);
                FadeTooltop(false, time);
                break;
            case SelectionState.Pressed:
                ThemeColor?.OnLightnessChange(Theme.ELightness.Dark, time);
                FadeTooltop(true, time);
                FadeShadowHeight(true, time);
                break;
            case SelectionState.Selected:
                ThemeColor?.OnLightnessChange(Theme.ELightness.Light, time);
                FadeTooltop(false, time);
                FadeShadowHeight(true, time);
                break;
            case SelectionState.Disabled:
                ThemeColor?.OnLightnessChange(Theme.ELightness.Disabled, time);
                FadeTooltop(false, time);
                break;
        }
    }

    protected void FadeShadowHeight(bool down, float time)
    {
        if (down)
        {
            if (UpRect != null)
                LeanTween.size(ShadowRect, UpRect.sizeDelta, time);
        }
        else
        {
            LeanTween.size(ShadowRect, DefaultShadowSize, time);
        }
    }

    protected void FadeTooltop(bool fadeIn, float time)
    {
        if (Tooltip == null) return;

        //float from = fadeIn ? 0.0f : 1.0f;
        float to = fadeIn ? 1.0f : 0.0f;
        LeanTween.alphaCanvas(Tooltip, to, time);
    }

    // Copied From Button
    public enum SelectionState
    {
        Normal = 0,
        Highlighted = 1,
        Pressed = 2,
        Selected = 3,
        Disabled = 4
    }
}
