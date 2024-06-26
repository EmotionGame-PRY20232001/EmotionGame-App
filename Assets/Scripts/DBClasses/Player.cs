using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [AutoIncrement, PrimaryKey]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool NeedsText { get; set; }
    public bool EnableAudio { get; set; } = true;
    public int BackgroundId { get; set; } = 0;
    public string GuideJSON { get; set; } = "";
    public uint StarsWon { get; set; } = 0;
    public Emotion.EEmotions EmotionsLearned { get; set; } = 0;
    public EmotionExercise.EActivity InstructionsRead { get; set; } = 0;
}
