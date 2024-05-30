using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Emotion
{
    [PrimaryKey]
    public Exercise.EEmotion Id;
    public Sprite SpriteColor;
    public Sprite SpriteGray;
    public Sprite Icon;
    public string Name;
    public Color Color;
    public List<Sprite> Faces;
    public List<string> Contexts;
}
