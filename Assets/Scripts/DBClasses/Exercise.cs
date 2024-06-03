using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Exercise
{
    [AutoIncrement, PrimaryKey]
    public int Id { get; set; }
    //[MaxLength(16)]
    public string ContentId { get; set; }
    public EmotionExercise.EActivity ActivityId { get; set; }
    //public uint ActivityId { get; set; }

}
