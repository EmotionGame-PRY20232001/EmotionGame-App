using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmotionExercise : MonoBehaviour
{
    public uint NumExcercises = 10;
    protected int CurrentExercise = 0;
    [SerializeField]
    public Emotion.EEmotion ExerciseEmotion { get; protected set; }
    protected List<Emotion.EEmotion> EmotionsToPractice;

    [SerializeField]
    protected PhotosCollection Exercises;

    protected virtual void Awake()
    {
        EmotionsToPractice = new List<Emotion.EEmotion>();
    }

    protected virtual void Start()
    {
        SetEmotionExercises();
        LoadCurrentEmotion();
    }

    protected virtual void SetEmotionExercises()
    {
        if (Exercises != null)
        {
            Exercises.LoadPhotos(NumExcercises);

            foreach (EmotionPhoto _photo in Exercises.Photos)
                LoadExercise(_photo);
        }
    }

    protected virtual void LoadExercise(EmotionPhoto photo)
    {
        if (photo == null) return;
        EmotionsToPractice.Add(photo.PhotoEmotion);
    }

    protected virtual void LoadCurrentEmotion()
    {
        if (CurrentExercise < EmotionsToPractice.Count) return;
        ExerciseEmotion = EmotionsToPractice.ElementAt(CurrentExercise);

        //EmotionPhoto photo = Exercises.Photos.ElementAt(CurrentExercise);
        //if (photo != null)
        //    ExerciseEmotion = photo.PhotoEmotion;
    }
}
