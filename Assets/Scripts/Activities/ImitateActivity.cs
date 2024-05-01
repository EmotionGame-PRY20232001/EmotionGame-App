using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImitateActivity : BaseActivity
{
    [SerializeField]
    private WebCamera webCam;
    [SerializeField]
    private FERModel Model;
    [SerializeField] 
    private float fillVelocity;
    [SerializeField]
    private Image emotionImageColor;
    [SerializeField]
    private Image emotionImageGray;

    private Exercise.EEmotion lastEmotion;

    protected override void Start()
    {
        base.Start();
    }

    protected override void LoadCurrentExercise(int newCurrent)
    {
        base.LoadCurrentExercise(newCurrent);
        StartCoroutine("CheckImitateEmotion");
        webCam.StartEmotionCoroutine();
        //StartCoroutine(CheckImitateEmotion());
    }

    protected override void StopCurrentExercise()
    {
        base.StopCurrentExercise();
        webCam.StopEmotionCoroutine();
        StopCoroutine("CheckImitateEmotion");
        //StopCoroutine(CheckImitateEmotion());
    }

    protected override void LoadEmotionButtons() { }

    IEnumerator CheckImitateEmotion()
    {
        while (true)
        {
            if (webCam.isRunning)
            {
                if (lastEmotion != Model.PredictedEmotion) emotionImageColor.fillAmount = 0;
                if (Model.PredictedEmotion != Exercise.EEmotion.Neutral)
                    emotionImageColor.fillAmount += fillVelocity * Time.deltaTime;
                if (emotionImageColor.fillAmount == 1)
                {
                    webCam.StopEmotionCoroutine();
                    yield return new WaitForSeconds(0.5f);
                    emotionImageColor.fillAmount = 0;
                    if (Model.PredictedEmotion == ExerciseEmotion) Good();
                    else Bad();
                }
                lastEmotion = Model.PredictedEmotion;
            }
            yield return null;
        }
    }
}
