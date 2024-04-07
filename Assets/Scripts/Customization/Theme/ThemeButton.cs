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

    protected SelectionState CurrentState = SelectionState.Normal;
    protected SelectionState PrevState = SelectionState.Normal;
    public SelectionState State
    {
        get { return CurrentState; }
        set { ChangeState(value, false); }
    }

    protected ThemeColorFilled ThemeColor;
    [SerializeField]
    protected float TransitionTime = 0.2f;
    [SerializeField]
    protected float AlphaOnDisabled = 0.5f;

    [SerializeField]
    protected RectTransform ShadowRect;
    [SerializeField]
    protected RectTransform UpRect;
    protected Vector2 DefaultShadowSize;

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

    protected void ChangeState(SelectionState state, bool instant)
    {
        if (state == PrevState) return;

        PrevState = CurrentState;
        CurrentState = state;
        float time = instant ? 0.0f : TransitionTime;

        switch (state)
        {
            case SelectionState.Normal:
                ThemeColor?.OnLightnessChange(Theme.ELightness.Main, time);
                LeanTween.size(ShadowRect, DefaultShadowSize, time);
                break;
            case SelectionState.Highlighted:
                ThemeColor?.OnLightnessChange(Theme.ELightness.Light, instant ? 0.0f : TransitionTime);
                break;
            case SelectionState.Pressed:
                ThemeColor?.OnLightnessChange(Theme.ELightness.Dark, time);
                if (UpRect != null)
                    LeanTween.size(ShadowRect, UpRect.sizeDelta, time);
                break;
            case SelectionState.Selected:
                ThemeColor?.OnLightnessChange(Theme.ELightness.Light, time);
                if (UpRect != null)
                    LeanTween.size(ShadowRect, UpRect.sizeDelta, time);
                break;
            case SelectionState.Disabled:
                ThemeColor?.OnLightnessChange(Theme.ELightness.Disabled, instant ? 0.0f : TransitionTime);
                break;
        }
    }


    ////////==== Animations ====////////
    //public void PlayAnimationNormal(bool instant = false)
    //{
    //}
    //public void PlayAnimationHighlighted(bool instant = false)
    //{
    //}
    //public void PlayAnimationPressed(bool instant = false)
    //{
    //}
    //public void PlayAnimationSelected(bool instant = false)
    //{
    //}
    //public void PlayAnimationDisabled(bool instant = false)
    //{
    //}


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
