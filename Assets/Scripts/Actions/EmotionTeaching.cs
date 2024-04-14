using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmotionTeaching : MonoBehaviour
{
    public uint NumExcercises = 10;
    protected uint CurrentExercise = 0;
    protected Emotion.EEmotion CurrentEmotion;
    List<Emotion.EEmotion> EmotionsToLearn;

    [SerializeField]
    protected PhotosCollection PhotosContent;
    [SerializeField]
    protected Stepper StepperCont;
    [SerializeField]
    protected Button BtnCancel;
    [SerializeField]
    protected Button BtnFinish;

    [SerializeField]
    protected Image ImageEmotion;
    [SerializeField]
    protected TMP_Text TextEmotion;

    void Start()
    {
        EmotionsToLearn = new List<Emotion.EEmotion>();
        SetEmotionExercises();

        if (BtnCancel != null)
            BtnCancel.interactable = true;
        if (BtnFinish != null)
            BtnFinish.interactable = false;
    }

    protected void SetEmotionExercises()
    {
        var gm = GameManager.Instance;

        if (PhotosContent != null)
        {
            PhotosContent.LoadPhotos(NumExcercises);

            EmotionPhoto photo = PhotosContent.Photos.ElementAt(0);
            if (photo != null)
                CurrentEmotion = photo.PhotoEmotion;

            foreach (EmotionPhoto _photo in PhotosContent.Photos)
                LoadExercise(_photo);
            StepperCont?.SetDefaultStep();
        }

        if (StepperCont != null)
        {
            StepperCont.onStepChange += OnEmotionChanged;
            StepperCont.onAllVisited += OnAllVisited;
        }
    }

    public void LoadExercise(EmotionPhoto photo)
    {
        if (photo == null) return;
        if (StepperCont != null)
            StepperCont.AddStep(photo.gameObject);

        EmotionsToLearn.Add(photo.PhotoEmotion);
    }

    public void OnEmotionChanged()
    {
        if (StepperCont == null) return;
        if (StepperCont.CurrentStep > EmotionsToLearn.Count) return;

        CurrentExercise = (uint)StepperCont.CurrentStep;
        CurrentEmotion = EmotionsToLearn.ElementAt(StepperCont.CurrentStep);
        Emotion.Data data = GameManager.Instance.Emotions[CurrentEmotion];

        if (ImageEmotion != null)
            ImageEmotion.sprite = data.Sprite;
        if (TextEmotion != null)
            TextEmotion.text = data.Name;
    }

    public void OnAllVisited()
    {
        if (BtnCancel != null)
            BtnCancel.interactable = false;

        if (BtnFinish != null)
            BtnFinish.interactable = true;
    }
}
