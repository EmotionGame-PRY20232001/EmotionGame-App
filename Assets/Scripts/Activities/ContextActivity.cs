using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContextActivity : BaseActivity
{
    [SerializeField]
    private TMP_Text ExerciseText;
    //[SerializeField]
    //private GameObject GridOfButtons;

    protected override void LoadExercise()
    {
        base.LoadExercise();
        LoadEmotionButtons(BtnEmotionPrefab);

        var gm = GameManager.Instance;
        var contextStrings = gm.Emotions[ExerciseEmotion].Contexts;
        ExerciseText.text = contextStrings[Random.Range(0, contextStrings.Count)];
    }
}
