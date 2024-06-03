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
    private GameObject BtnCameraImgCheck;
    [SerializeField]
    private GameObject BtnCameraImgReset;
    [SerializeField]
    private Button BtnReady;
    [SerializeField]
    private GameObject BtnReadyImgEmotion;
    [SerializeField]
    private RawImage RawImgPhoto;
    protected bool IsFirstPhoto;

    private Emotion.EEmotion lastEmotion;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
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

    private void ShowBtnCameraCheck(bool value)
    {
        if (BtnCameraImgCheck != null)
            BtnCameraImgCheck.SetActive(value);
        if (BtnCameraImgReset != null)
            BtnCameraImgReset.SetActive(!value);
    }

    protected override void LoadEmotionButtons()
    {
        // Setting defaults for Emotion and Ready Button
        ShowBtnCameraCheck(true);
        IsFirstPhoto = true;

        if (BtnReady != null)
            BtnReady.interactable = false;
        if (RawImgPhoto != null)
            RawImgPhoto.color = Color.clear;
        if (BtnReadyImgEmotion != null)
            BtnReadyImgEmotion.SetActive(false);
    }

    public void TakePhoto()
    {
        webCam.StopEmotionCoroutine();
        webCam.PauseCamera();

        if (IsFirstPhoto)
        {
            ShowBtnCameraCheck(false);
            if (BtnReady != null)
                BtnReady.interactable = true;
            if (RawImgPhoto != null)
                RawImgPhoto.color = Color.white;
            if (BtnReadyImgEmotion != null)
                BtnReadyImgEmotion.SetActive(true);
            IsFirstPhoto = false;
        }

        if (RawImgPhoto != null)
            RawImgPhoto.texture = webCam.rawImage.texture;
    }

    public void CheckExercise()
    {
        if (Model.PredictedEmotion == ExerciseEmotion) Good();
        else Bad(Model.PredictedEmotion);
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
