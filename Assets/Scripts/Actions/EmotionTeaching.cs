using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmotionTeaching : EmotionExercise
{
    [SerializeField]
    protected Stepper StepperCont;
    [SerializeField]
    protected Button BtnCancel;
    [SerializeField]
    protected Button BtnFinish;

    [SerializeField]
    protected EmotionObject Emotion;

    protected override void Start()
    {
        base.Start();

        if (BtnCancel != null)
            BtnCancel.interactable = true;
        if (BtnFinish != null)
            BtnFinish.interactable = false;
    }

    protected override void SetEmotionExercises()
    {
        base.SetEmotionExercises();

        if (StepperCont != null)
        {
            StepperCont.onStepChange += OnEmotionChanged;
            StepperCont.onAllVisited += OnAllVisited;
            StepperCont.SetDefaultStep(0);
        }
    }

    protected override void LoadExercise(EmotionPhoto photo)
    {
        if (photo == null) return;
        if (StepperCont != null)
            StepperCont.AddStep(photo.gameObject);
        base.LoadExercise(photo);
    }

    public void OnEmotionChanged()
    {
        if (StepperCont == null
            || StepperCont.CurrentStep < 0
            || StepperCont.CurrentStep > EmotionsToPractice.Count)
            return;

        CurrentExercise = StepperCont.CurrentStep;
        ExerciseEmotion = EmotionsToPractice.ElementAt(StepperCont.CurrentStep);

        if (Emotion != null)
            Emotion.SetEmotion(ExerciseEmotion);
    }

    public void OnAllVisited()
    {
        if (BtnCancel != null)
            BtnCancel.interactable = false;

        if (BtnFinish != null)
            BtnFinish.interactable = true;
    }
}
