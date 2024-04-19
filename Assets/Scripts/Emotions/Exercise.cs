using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;

[System.Serializable]
public class Exercise : MonoBehaviour
{
    public enum EEmotion { Anger, Disgust, Fear, Happy, Neutral, Sad, Surprise }
    public enum EActivity { None, Choose, Context, Imitate }

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
        public Sprite Sprite;
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