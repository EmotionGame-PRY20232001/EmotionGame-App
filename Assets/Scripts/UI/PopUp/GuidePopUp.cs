using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PopUp))]
public class GuidePopUp : MonoBehaviour
{
    protected PopUp popUp;
    [SerializeField]
    protected RectTransform Guide;
    [SerializeField]
    protected Button PlayButton;
    [SerializeField]
    protected Stepper InstStepper;
    //public float ScaleGuideTo = 0.75f;

    private void Awake()
    {
        popUp = GetComponent<PopUp>();
        popUp.onOpen += delegate { EnableGuide(true); };
        popUp.onClose += delegate { EnableGuide(false); };
    }

    void Start()
    {
        CheckIfUnlockPlay();
    }

    /// <summary>
    /// Locks PlayButton if player has never played that game
    /// If no player active, blocked by default
    /// </summary>
    public void CheckIfUnlockPlay()
    {
        if (PlayButton == null) return;
        PlayButton.interactable = GetIfUnlockPlay();

        if (InstStepper != null)
            InstStepper.onAllVisited += SetInstructionsRead;
    }

    public void SetInstructionsRead()
    {
        if (PlayButton != null)
            PlayButton.interactable = true;

        var gm = GameManager.Instance;
        if (gm != null && gm.IsPlayerActive() && DBManager.Instance != null)
        {
            gm.currentPlayer.InstructionsRead |= BaseActivity.Instance.Activity;
            DBManager.Instance.UpdatePlayerToDb(gm.currentPlayer);
        }
    }

    protected bool GetIfUnlockPlay()
    {
        var gm = GameManager.Instance;

        if (gm == null || !gm.IsPlayerActive() ||
            BaseActivity.Instance == null)
            return false;

        EmotionExercise.EActivity activities = gm.GetCurrentPlayer().InstructionsRead;
        if (activities == EmotionExercise.EActivity.None) return false;

        return activities.HasFlag(BaseActivity.Instance.Activity);
    }

    /// Guide
    protected void EnableGuide(bool value)
    {
        if (Guide == null) return;

        LeanTween.alpha(Guide, value ? 1.0f : 0.0f, popUp.TransitionTime/2)
                .setIgnoreTimeScale(true);
        LeanTween.moveY(Guide.gameObject, Guide.position.y, popUp.TransitionTime)
                .setFrom(-Guide.position.y)
                .setIgnoreTimeScale(true);
        //LeanTween.scale(Guide, Vector3.one * ScaleGuideTo * (value ? 1.0f : 0.5f), popUp.TransitionTime / 2)
        //        .setIgnoreTimeScale(true);
    }

}
