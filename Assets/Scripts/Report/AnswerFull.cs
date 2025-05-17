using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnswerFull : Answer
{
    [SerializeField]
    protected TMPro.TMP_Text txtDateTime;
    [SerializeField]
    protected TMPro.TMP_Text txtDuration;
    [SerializeField]
    protected TMPro.TMP_Text txtCorrectEmotion;
    [SerializeField]
    protected TMPro.TMP_Text txtSelectedEmotion;
    [SerializeField]
    protected RectTransform ContextExercise;

    // Imitation
    [SerializeField]
    protected RectMask2D PlayerMask;
    // Imitation - Edit
    [SerializeField]
    protected AnswerEditEmotion EmotionEditObj;
    [SerializeField]
    protected PopUp EmotionEditPopUp;
    [SerializeField]
    protected Button PlayerEditEmotion;
    [SerializeField]
    protected Button PlayerBtnShow;
    [SerializeField]
    protected Button PlayerBtnHide;
    [field: SerializeField]
    public EPhotoMode PhotoMode { get; protected set; } = EPhotoMode.HALF;

    public enum EPhotoMode { HALF, PLAYER, EXERCISE };

    protected void Awake()
    {
        PlayerBtnShow?.onClick.AddListener(OnBtnShowClick);
        PlayerBtnHide?.onClick.AddListener(OnBtnHideClick);
        PlayerEditEmotion?.onClick.AddListener(OnOpenEditPopUp);
    }

    public override void Load(ReportManager.FullResponse r)
    {
        base.Load(r);
        LoadTime(r.response.CompletedAt, r.response.SecondsToSolve);

        bool isImitate = r.exercise.ActivityId == EmotionExercise.EActivity.Imitate;
        if (isImitate)
        {
            EmotionEditObj?.LoadEmotionDetected(r);
            ImitateResetToHalf();
        }

        PlayerEditEmotion?.gameObject.SetActive(isImitate);
        PlayerBtnShow?.gameObject.SetActive(isImitate);
        PlayerBtnHide?.gameObject.SetActive(isImitate);
    }

    public virtual void LoadTime(System.DateTime completedAt, float secondsToSolve)
    {
        if (txtDateTime != null)
        {
            txtDateTime.text = completedAt.ToShortDateString() + " "
                               + completedAt.ToShortTimeString();
        }

        if (txtDuration != null)
        {
            System.TimeSpan duration = System.TimeSpan.FromSeconds(secondsToSolve);
            txtDuration.text = string.Format("{0:mm\\:ss\\:fff}", duration);
        }
    }

    public override void LoadEmotion(Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        base.LoadEmotion(correctEmotion, responseEmotion);
        var gm = GameManager.Instance;

        if (txtCorrectEmotion != null)
            txtCorrectEmotion.text = gm.Emotions[correctEmotion].Name;

        if (txtSelectedEmotion != null)
            txtSelectedEmotion.text = gm.Emotions[responseEmotion].Name;
    }

    public override void LoadChoose(Sprite exercisePhoto, Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        Photo.enabled = true;
        ContextExercise.gameObject.SetActive(false);
        PlayerPhoto.gameObject.SetActive(false);
        base.LoadChoose(exercisePhoto, correctEmotion, responseEmotion);
    }

    public override void LoadContext(string text, Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        Photo.enabled = false;
        ContextExercise.gameObject.SetActive(true);
        PlayerPhoto.gameObject.SetActive(false);
        base.LoadContext(text, correctEmotion, responseEmotion);
    }

    public override void LoadImitate(Sprite exercisePhoto, Sprite playerPhoto, Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        Photo.enabled = true;
        ContextExercise.gameObject.SetActive(false);
        PlayerPhoto.gameObject.SetActive(true);
        base.LoadImitate(exercisePhoto, playerPhoto, correctEmotion, responseEmotion);
    }

    public void OnBtnShowClick()
    {
        if (PhotoMode == EPhotoMode.HALF)
            ImitateShowFull(true);
        else
            ImitateResetToHalf();
    }

    public void OnBtnHideClick()
    {
        if (PhotoMode == EPhotoMode.HALF)
            ImitateShowFull(false);
        else
            ImitateResetToHalf();
    }

    public void ImitateShowFull(bool isPlayer)
    {
        if (PhotoMode != EPhotoMode.HALF) return;

        PhotoMode = isPlayer ? EPhotoMode.PLAYER : EPhotoMode.EXERCISE;

        PlayerMask.enabled = !isPlayer;
        PlayerEditEmotion.gameObject.SetActive(isPlayer);
        PlayerPhoto.gameObject.SetActive(isPlayer);

        PlayerBtnShow.gameObject.SetActive(isPlayer);
        PlayerBtnShow.gameObject.LeanScaleX(isPlayer ? - 1 : 1, 0.0f);
        PlayerBtnHide.gameObject.SetActive(!isPlayer);
        PlayerBtnHide.gameObject.LeanScaleX(isPlayer ? 1 : -1, 0.0f);
    }

    public void ImitateResetToHalf()
    {
        if (PhotoMode == EPhotoMode.HALF) return;

        PhotoMode = EPhotoMode.HALF;

        PlayerMask.enabled = true;
        PlayerEditEmotion.gameObject.SetActive(true);
        PlayerPhoto.gameObject.SetActive(true);

        PlayerBtnShow.gameObject.SetActive(true);
        PlayerBtnShow.gameObject.LeanScaleX(1, 0.0f);
        PlayerBtnHide.gameObject.SetActive(true);
        PlayerBtnHide.gameObject.LeanScaleX(1, 0.0f);
    }

    // Imitate edit emotion
    protected void OnOpenEditPopUp()
    {
        EmotionEditPopUp?.Open();
    }
}
