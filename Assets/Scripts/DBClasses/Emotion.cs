using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;

[System.Serializable]
public class Emotion
{
    public enum EEmotion : uint { Anger, Disgust, Fear, Happy, Neutral, Sad, Surprise }
    
    [System.Flags]
    public enum EEmotions : uint
    {
        Neutral = 0,
        Anger   = 1 << 0,
        Disgust = 1 << 1,
        Fear    = 1 << 2,
        Happy   = 1 << 3,
        Sad     = 1 << 4,
        Surprise = 1 << 5,

        //Combinations
        Easy = Happy | Sad,
        Medium = Easy | Anger | Surprise,
        Hard = Medium | Fear | Disgust, //All
    }

    public Sprite SpriteColor;
    public Sprite SpriteGray;
    public Sprite Icon;
    public string Name;
    public Color Color;
    public ExerciseContent ExerciseContents;

    // [System.Flags]
    // public enum EFEmotions
    // {
    //     Happy    = 0,
    //     Sad      = 1 << 0,
    //     Anger    = 1 << 1,
    //     Disgust  = 1 << 2,
    //     Fear     = 1 << 3,
    //     Surprise = 1 << 4
    // }

}