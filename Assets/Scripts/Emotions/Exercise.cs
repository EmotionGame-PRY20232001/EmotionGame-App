using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;

[System.Serializable]
public class Exercise : MonoBehaviour
{
    public enum EEmotion { Anger, Disgust, Fear, Happy, Neutral, Sad, Surprise }
    public enum EActivity { None, Learn, Choose, Context, Imitate }
    
    [System.Flags]
    public enum EEmotions
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


    [System.Serializable]
    public struct Emotion
    {
        public Sprite SpriteColor;
        public Sprite SpriteGray;
        public Sprite Icon;
        public string Name;
        public Color Color;
        public List<Sprite> Faces;
        public List<string> Contexts;
    }

    [System.Serializable]
    public struct Activity
    {
        public EActivity Game;
        public string Name;
        public Sprite Sprite;
    }


    // ID
    [AutoIncrement, PrimaryKey]
    public int Id { get; set; }
    EActivity Game { get; set; }
    EEmotion CorrectEmotion { get; set; }
    string ImageName { get; set; }
    string Text { get; set; }

}