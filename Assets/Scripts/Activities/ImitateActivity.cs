using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImitateActivity : BaseActivity
{
    [SerializeField]
    private FERModel Model;

    protected override void LoadExercise()
    {
        base.LoadExercise();
        StartCoroutine(CheckImitateEmotion());
    }

    protected override void LoadEmotionButtons() { }

    IEnumerator CheckImitateEmotion()
    {
        while (true)
        {
            if (Model.PredictedEmotion == ExerciseEmotion)
            {
                Debug.Log("Sí es");
                //LoadImitateExercise();
                Good();
            }
            yield return null;
        }
    }
}
