using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompletedActivity : MonoBehaviour
{
    [SerializeField]
    protected GameObject StarPrefab;
    [SerializeField]
    protected GameObject Riborn;
    [SerializeField]
    protected TMP_Text TxtScore;
    [SerializeField]
    protected TMP_Text TxtNumGood;
    [SerializeField]
    protected TMP_Text TxtMotivation;

    readonly uint NumMaxStars = 5;
    [SerializeField]
    protected RectTransform StarsLayout;
    [SerializeField]
    protected float StarScaleClose = 0.95f;
    [SerializeField]
    protected float StarScaleFar = 0.85f;

    void Start()
    {
        AnimateRibornEntrance();
        LoadTexts();
        LoadStars();
    }

    protected void AnimateRibornEntrance()
    {
    }
    protected void AnimateRibornLoop()
    {
    }

    protected void LoadTexts()
    {
        var gm = GameManager.Instance;
        if (gm == null) return;

        if (TxtScore != null)
        {
            int score = gm.LastScore;
            if (score > 0)
                TxtScore.text = "¡" + score + " puntos!";
        }

        uint numGood = gm.LastNumCorrectAnswers;
        if (TxtNumGood != null)
        {
            if (numGood > 0)
            {
                TxtNumGood.text = "¡" + numGood + " de " + gm.LastNumExcercises + " correctas!";
                if (numGood > 5)
                    TxtNumGood.text = "¡Increíble! " + TxtNumGood.text;

                if (TxtMotivation != null)
                    TxtMotivation.enabled = false;
            }
            else
            {
                TxtNumGood.enabled = false;
                TxtScore.enabled = false;

                //TxtMotivation.enabled = true;
                if (TxtMotivation != null)
                    TxtNumGood.enabled = false;
            }
        }
    }

    protected void LoadStars()
    {
        if (StarPrefab == null || StarsLayout == null) return;

        var gm = GameManager.Instance;
        if (gm == null) return;

        uint numGood = gm.LastNumCorrectAnswers;
        uint total = gm.LastNumExcercises;
        if (total == 0) return;

        uint starPerExercises = total / NumMaxStars;
        uint currentStars = numGood / starPerExercises;
        uint halfStars = (uint)Mathf.CeilToInt(currentStars / 2);

        if (currentStars % 2 == 0)
        {
            // 1  .2.  .3.  4    |  4/2 = 2
            float halfDec = (halfStars * 2 + 1) / 2;
            for (uint i = 1; i <= currentStars; i++)
            {
                var star = Instantiate(StarPrefab, StarsLayout);
                if (Mathf.Abs(i - halfDec) > 0.6f)
                {
                    bool closeToHalf = Mathf.Abs(i - halfDec) <= 1.1f;
                    star.transform.localScale = Vector3.one * (closeToHalf ? StarScaleClose : StarScaleFar);
                }
            }
        }
        else
        {
            // 1  .2.  >3<  .4.  5    |  5/2 = 2.5...3
            for (uint i = 1; i <= currentStars; i++)
            {
                var star = Instantiate(StarPrefab, StarsLayout);
                if (i != halfStars)
                {
                    bool closeToHalf = Mathf.Abs(i - halfStars) <= 1.1f;
                    star.transform.localScale = Vector3.one * (closeToHalf ? StarScaleClose : StarScaleFar);
                }
            }
        }
    }
}
