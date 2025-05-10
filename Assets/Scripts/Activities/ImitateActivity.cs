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

    //[SerializeField]
    //private Image emotionImageColor;
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

    protected override void Awake()
    {
        base.Awake();
        Activity = EActivity.Imitate;
    }

    protected override void Start()
    {
        base.Start();
#if UNITY_ANDROID && !UNITY_EDITOR
        RawImgPhoto.transform.Rotate(Vector3.forward, 90);
        RawImgPhoto.transform.Rotate(Vector3.right, 180);
#endif
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
        if (!IsFirstPhoto)
        {
            webCam.PlayCamera();
            webCam.StartEmotionCoroutine();
        }
        else
        {
            webCam.StopEmotionCoroutine();
            webCam.PauseCamera();
        }

        ShowBtnCameraCheck(!IsFirstPhoto);
        
        if (BtnReady != null)
            BtnReady.interactable = IsFirstPhoto;
        if (RawImgPhoto != null)
            RawImgPhoto.color = IsFirstPhoto ? Color.white : Color.clear;
        if (BtnReadyImgEmotion != null)
            BtnReadyImgEmotion.SetActive(IsFirstPhoto);

        if (RawImgPhoto != null)
            RawImgPhoto.texture = webCam.rawImage.texture;

        IsFirstPhoto = !IsFirstPhoto;
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

    protected void SavePhoto()
    {
        if (RawImgPhoto == null) return;

        // Ensure the texture is a Texture2D
        Texture2D sourceTexture = RawImgPhoto.texture as Texture2D;
        if (sourceTexture == null)
        {
            Debug.LogWarning("Texture is not a Texture2D. Try converting it first.");
            sourceTexture = GetReadableTexture(RawImgPhoto.texture, 0.5f);
        }

        if (sourceTexture == null)
        {
            Debug.LogWarning("Failed to create readable texture.");
            return;
        }

        // Encode to JPG
        int quality = 67;
        byte[] jpgBytes = sourceTexture.EncodeToJPG(quality); //readableTex

        // Save to disk
        string path = Utils.GetDefaultFilePathName("Photos", "jpg", CurrentExerciseDBO.Id.ToString());
        System.IO.File.WriteAllBytes(path, jpgBytes);
        Debug.Log("Saved JPG to: " + path);

        // Clean up
        Object.Destroy(sourceTexture);
    }

    protected Texture2D GetReadableTexture(Texture sourceTexture, float scale = 1.0f)
    {
        // Use source size
        int width = sourceTexture.width;
        int height = sourceTexture.height;

        // Create a temporary RenderTexture
        RenderTexture rtOriginal = RenderTexture.GetTemporary(width, height, 0);
        Graphics.Blit(sourceTexture, rtOriginal);

        RenderTexture rtNew = rtOriginal;
        if (scale != 1.0f)
        {
            // Create a scaled RenderTexture
            width = (int)(width * scale);
            height = (int)(height * scale);
            rtNew = RenderTexture.GetTemporary(width, height, 0);

            // Blit original RenderTexture into the new one (this scales it)
            Graphics.Blit(rtOriginal, rtNew);
            RenderTexture.ReleaseTemporary(rtOriginal); // Free the original now
        }

        // Backup the current active RenderTexture
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rtNew;

        // Create a new readable Texture2D and copy pixels
        Texture2D readableTex = new Texture2D(width, height, TextureFormat.RGB24, false);
        readableTex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        readableTex.Apply();

        // Restore state and clean up
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rtNew);

        return readableTex;
    }
}
