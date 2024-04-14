using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (ActivityManager.Instance != null)
        {
            if (CurrEmotion == ActivityManager.Instance.ExerciseEmotion) ActivityManager.Instance.Good();
            else ActivityManager.Instance.Bad();
        }
    }

}