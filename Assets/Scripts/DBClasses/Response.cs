using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response
{
    [AutoIncrement, PrimaryKey]
    public int Id { get; set; }
    public int UserId { get; set; } //PlayerId
    public System.DateTime CompletedAt { get; set; }
    public int ExerciseId { get; set; }
    public Emotion.EEmotion ResponseEmotionId { get; set; } //uint
    // numAlerts
    // numHelp
    public float SecondsToSolve { get; set; }
    public bool IsCorrect { get; set; }

    public override string ToString()
    {
        return "Player_" + UserId +
            " CompletedAt_" + CompletedAt +
            " ExerciseId_" + ExerciseId +
            " ResponseEmotionId_" + ResponseEmotionId +
            " Seconds_" + SecondsToSolve +
            " Correct_" + IsCorrect;
    }
}
