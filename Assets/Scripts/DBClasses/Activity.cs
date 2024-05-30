using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Activity
{
    [PrimaryKey]
    public Exercise.EActivity Id;
    public string Name;
    public Sprite Sprite;
}
