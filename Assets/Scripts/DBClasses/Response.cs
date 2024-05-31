using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response
{
    public int UserId { get; set; }

    public int CompletedAt { get; set; }

    public string ExerciseId { get; set; }

    public uint ActivityId { get; set; }

    public uint ResponseEmotionId { get; set; }

    public float SecondsToSolve { get; set; }

    public bool IsCorrect { get; set; }

}
