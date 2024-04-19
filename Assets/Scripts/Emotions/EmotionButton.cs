using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Button))]
public class EmotionButton : EmotionObject
{
    protected Button Button;
    
    protected override void Awake()
    {
        base.Awake();
        Button = gameObject.GetComponent<Button>();
    }

    protected override void Start()
    {
    }

    public void PressButton()
    {
        if (BaseActivity.Instance != null)
        {
            if (CurrEmotion == BaseActivity.Instance.ExerciseEmotion) BaseActivity.Instance.Good();
            else BaseActivity.Instance.Bad();
        }
    }

}