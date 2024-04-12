using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmotionTeaching : MonoBehaviour
{
    public uint NumExercises = 10;
    protected uint CurrentExercise = 0;
    protected Emotion.EEmotion CurrentEmotion;

    [SerializeField]
    protected bool UseRandom = true;

    [SerializeField]
    protected EmotionPhoto PhotoPrefab;
    //[SerializeField]
    //protected List<GameObject> Photos;
    protected List<Emotion.EEmotion> EmotionsToLearn;
    [SerializeField]
    protected GameObject PhotosContent;
    [SerializeField]
    protected Stepper StepperCont;

    [SerializeField]
    protected Image ImageEmotion;
    [SerializeField]
    protected TMP_Text TextEmotion;

    void Start()
    {
        SetEmotionExercises();
    }

    protected void SetEmotionExercises()
    {
        Dictionary<Emotion.EEmotion, uint> EmotionExercises;
        EmotionExercises = new Dictionary<Emotion.EEmotion, uint>();
        EmotionsToLearn = new List<Emotion.EEmotion>();

        var gm = GameManager.Instance;
        uint numEmotions = (uint)(gm.SelectedEmotions.Count);
        uint numPerEmotion = NumExercises / numEmotions;

        foreach (Emotion.EEmotion emotion in gm.SelectedEmotions)
        {
            EmotionExercises[emotion] = numPerEmotion;
        }

        if (NumExercises % numEmotions != 0)
        {
            uint aux = NumExercises - (numEmotions * numPerEmotion);
            var emotions = gm.SelectedEmotions.OrderBy(x => Random.value).ToList();

            for (int i = 0; i < aux; i++)
            {
                Emotion.EEmotion emotion = emotions.ElementAt(i);
                EmotionExercises[emotion] += 1;
            }
        }

        CurrentEmotion = gm.SelectedEmotions[0];
        for (uint i = 0; i < NumExercises; i++)
            LoadExercise(EmotionExercises);

        if (StepperCont != null)
            StepperCont.onStepChange += OnEmotionChanged;
    }

    public void LoadExercise(Dictionary<Emotion.EEmotion, uint> EmotionExercises)
    {
        List<Emotion.EEmotion> emotionsLeft = EmotionExercises.Keys.ToList();
        if (UseRandom)
        {
            emotionsLeft.Remove(CurrentEmotion);
            CurrentEmotion = emotionsLeft[Random.Range(0, emotionsLeft.Count)];
        }
        else
        {
            CurrentEmotion = emotionsLeft[0];
        }


        if (PhotoPrefab != null && PhotosContent != null)
        {
            var photo = Instantiate(PhotoPrefab, PhotosContent.transform);
            if (photo != null)
            {
                photo.SetPhotoEmotion(CurrentEmotion);
                if (StepperCont != null)
                    StepperCont.AddStep(photo.gameObject);
            }
        }

        EmotionExercises[CurrentEmotion] -= 1;
        if (EmotionExercises[CurrentEmotion] == 0)
        {
            EmotionExercises.Remove(CurrentEmotion);
        }

        EmotionsToLearn.Add(CurrentEmotion);
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
}
