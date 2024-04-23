using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImitateActivity : BaseActivity
{
    [SerializeField]
    private FERModel Model;
    protected override void LoadCurrentExercise(int newCurrent)
    {
        base.LoadCurrentExercise(newCurrent);
        StartCoroutine(CheckImitateEmotion());
    }

    protected override void StopCurrentExercise()
    {
        base.StopCurrentExercise();
        StopCoroutine(CheckImitateEmotion());
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
            yield return new WaitForSeconds(0.3f);
        }
    }
}
