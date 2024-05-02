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

    private Exercise.EEmotion lastEmotion;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (webCam.isRunning)
        {
            lastEmotion = Model.PredictedEmotion;
        }
    }

    protected override void LoadCurrentExercise(int newCurrent)
    {
        base.LoadCurrentExercise(newCurrent);
        webCam.StartEmotionCoroutine();
    }

    protected override void StopCurrentExercise()
    {
        base.StopCurrentExercise();
        webCam.StopEmotionCoroutine();
    }

    protected override void LoadEmotionButtons() { }

    public void TakePhoto()
    {
        webCam.StopEmotionCoroutine();
        webCam.PauseCamera();
    }

    public void CheckExercise()
    {
        if (Model.PredictedEmotion == ExerciseEmotion) Good();
        else Bad();
        webCam.PlayCamera();
    }

    //IEnumerator CheckImitateEmotion()
    //{
    //    while (true)
    //    {
    //        if (webCam.isRunning)
    //        {
    //            if (lastEmotion != Model.PredictedEmotion) emotionImageColor.fillAmount = 0;
    //            if (Model.PredictedEmotion != Exercise.EEmotion.Neutral)
    //                emotionImageColor.fillAmount += fillVelocity * Time.deltaTime;
    //            if (emotionImageColor.fillAmount == 1)
    //            {
    //                webCam.StopEmotionCoroutine();
    //                yield return new WaitForSeconds(0.5f);
    //                emotionImageColor.fillAmount = 0;
    //                if (Model.PredictedEmotion == ExerciseEmotion) Good();
    //                else Bad();
    //            }
    //            lastEmotion = Model.PredictedEmotion;
    //        }
    //        yield return null;
    //    }
    //}
}
