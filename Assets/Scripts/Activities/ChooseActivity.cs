using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseActivity : BaseActivity
{
    protected override void Awake()
    {
        base.Awake();
        Activity = Exercise.EActivity.Choose;
    }
    protected override void LoadExercise()
    {
        base.LoadExercise();
        LoadEmotionButtons(BtnEmotionPrefab);
    }
}
