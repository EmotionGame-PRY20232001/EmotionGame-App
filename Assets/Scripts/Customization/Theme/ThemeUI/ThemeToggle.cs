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
    protected Toggle m_Toggle;
    protected float TrackThumbDiff = 0.0f;
    [SerializeField]
    protected SpriteToogle IconThumb;

    ////////==== Unity ====////////
    protected override void Awake()
    {
        base.Awake();
        m_Toggle = gameObject.GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });
    }

    protected override void Start()
    {
        CalcTrackThumDiff();
        base.Start();
    }

    protected virtual void ToggleValueChanged(Toggle toggle)
    {
        ChangeState(SelectionState.Normal, false);
    }

    protected virtual void CalcTrackThumDiff() { }


    ////////==== State ====////////
    //OFF | Normal > Highlighted > Pressed
    //ON  | Selected > Highlighted > Normal > Presed
    //OFF | ^
    //---------
    //ON  | Normal > Highlighted > Pressed
    //OFF | Selected > Highlighted > Normal > Presed
    //ON  | ^
    protected override void PlayAnimationNormal(float time)
    {
        ThemeColor?.OnLightnessChange(m_Toggle.isOn ? Theme.ELightness.Dark : Theme.ELightness.Main, time);
        FadeTooltop(false, time);

        OnSetActiveAnimation(m_Toggle.isOn, time);
    }
    protected override void PlayAnimationHighlighted(float time)
    {
        ThemeColor?.OnLightnessChange(m_Toggle.isOn ? Theme.ELightness.Main : Theme.ELightness.Light, time);
    }
    protected override void PlayAnimationPressed(float time)
    {
        ThemeColor?.OnLightnessChange(m_Toggle.isOn ? Theme.ELightness.Main : Theme.ELightness.Dark, time);
        FadeTooltop(false, time);

        OnSetActiveAnimation(!m_Toggle.isOn, time);
    }
    protected override void PlayAnimationSelected(float time)
    {
        FadeTooltop(false, time);
        OnSetActiveAnimation(m_Toggle.isOn, time);
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
