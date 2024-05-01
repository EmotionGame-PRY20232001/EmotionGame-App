using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(ThemeColorFilled))]
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
    //[SerializeField]
    //protected float AlphaOnDisabled = 0.5f;

    //---- Transform ----//
    [SerializeField]
    protected RectTransform ShadowRect;
    [SerializeField]
    protected RectTransform UpRect;
    protected Vector2 DefaultShadowSize;

    //---- Tooltip ----//
    [SerializeField]
    protected CanvasGroup Tooltip;

    public delegate void OnNormalState();
    public OnNormalState onNormalState;

    ////////==== Unity ====////////
    protected virtual void Awake()
    {
        ThemeColor = gameObject.GetComponent<ThemeColorFilled>();
    }

    protected virtual void Start()
    {
        if (ShadowRect != null)
        {
            DefaultShadowSize = ShadowRect.sizeDelta;
        }

        ChangeState(SelectionState.Normal, true);
    }

    ////////==== State ====////////
    protected virtual void ChangeState(SelectionState state, bool instant)
    {
        if (state != SelectionState.Normal && CurrentState == state) return;

        CurrentState = state;
        float time = instant ? 0.0f : TransitionTime;

        switch (state)
        {
            case SelectionState.Normal:
                PlayAnimationNormal(time);
                onNormalState?.Invoke();
                break;
            case SelectionState.Highlighted:
                PlayAnimationHighlighted(time);
                break;
            case SelectionState.Pressed:
                PlayAnimationPressed(time);
                break;
            case SelectionState.Selected:
                PlayAnimationSelected(time);
                break;
            case SelectionState.Disabled:
                PlayAnimationDisabled(time);
                break;
        }
    }

    protected virtual void OnSetActiveAnimation(bool isActive, float time)
    {
        if (ShadowRect == null) return;
        if (isActive)
        {
            if (UpRect != null)
                LeanTween.size(ShadowRect, UpRect.sizeDelta, time);
        }
        else
        {
            LeanTween.size(ShadowRect, DefaultShadowSize, time);
        }
    }

    protected void FadeTooltop(bool show, float time)
    {
        if (Tooltip == null) return;

        //float from = show ? 0.0f : 1.0f;
        float to = show ? 1.0f : 0.0f;
        LeanTween.alphaCanvas(Tooltip, to, time);
    }

    protected virtual void PlayAnimationNormal(float time)
    {
        ThemeColor?.OnLightnessChange(Theme.ELightness.Main, time);
        FadeTooltop(false, time);
        OnSetActiveAnimation(false, time);
    }
    protected virtual void PlayAnimationHighlighted(float time)
    {
        ThemeColor?.OnLightnessChange(Theme.ELightness.Light, time);
        FadeTooltop(false, time);
    }
    protected virtual void PlayAnimationPressed(float time)
    {
        ThemeColor?.OnLightnessChange(Theme.ELightness.Dark, time);
        FadeTooltop(true, time);
        OnSetActiveAnimation(true, time);
    }
    protected virtual void PlayAnimationSelected(float time)
    {
        ThemeColor?.OnLightnessChange(Theme.ELightness.Light, time);
        FadeTooltop(false, time);
        OnSetActiveAnimation(true, time);
    }
    protected virtual void PlayAnimationDisabled(float time)
    {
        ThemeColor?.OnLightnessChange(Theme.ELightness.Disabled, time);
        FadeTooltop(false, time);
    }

    // Copied From Button
    public enum SelectionState
    {
        Normal = 0,
        Highlighted = 1,
        Pressed = 2,
        Selected = 3,
        Disabled = 4,
    }
}
