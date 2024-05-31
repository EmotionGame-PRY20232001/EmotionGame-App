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
    public int ContentId { get; set; }
    public EmotionExercise.EActivity Activity { get; set; }

}
