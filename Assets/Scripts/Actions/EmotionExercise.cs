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
            NumExcercises = (uint)Exercises.Photos.Count;
            foreach (EmotionPhoto _photo in Exercises.Photos)
                LoadExercise(_photo);
        }
    }

    protected virtual void LoadExercise(EmotionPhoto photo)
    {
        if (photo != null)
            EmotionsToPractice.Add(photo.PhotoEmotion);
    }

    protected virtual void LoadCurrentEmotion()
    {
        if (CurrentExercise >= EmotionsToPractice.Count) return;
        ExerciseEmotion = EmotionsToPractice.ElementAt(CurrentExercise);

        //EmotionPhoto photo = Exercises.Photos.ElementAt(CurrentExercise);
        //if (photo != null)
        //    ExerciseEmotion = photo.PhotoEmotion;
    }


    [System.Flags]
    public enum EActivity : byte
    {
        None    = 0,
        Learn   = 1 << 0,
        Choose  = 1 << 1,
        Context = 1 << 2,
        Imitate = 1 << 3,
    }


    [System.Serializable]
    public struct Data
    {
        //[PrimaryKey]
        public EActivity Id { get; set; }
        public string Name { get; set; }
        public Sprite Sprite { get; set; }
    }
}
