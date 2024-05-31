using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//[Table("ExerciseContent")]
public class ExerciseContent
{
    //[AutoIncrement, PrimaryKey]
    //public int Id { get; set; }
    //[Indexed(Name = "Content", Order = 1, Unique = true)]
    //public string Face { get; set; }
    //[Indexed(Name = "Content", Order = 2, Unique = true)]
    //public string Text { get; set; }
    //Exercise.EEmotion CorrectEmotion { get; set; }

    //id is order on list
    public string Value;
    public bool IsPath;

    
}
