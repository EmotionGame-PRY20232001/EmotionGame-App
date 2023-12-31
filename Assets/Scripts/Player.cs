using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    [AutoIncrement, PrimaryKey]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool NeedsText { get; set; }
    public int BackgroundId { get; set; }
}
