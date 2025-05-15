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

    readonly sbyte NumMaxStars = 5;
    [SerializeField]
    protected RectTransform StarsLayout;
    [SerializeField]
    protected float StarScaleClose = 0.95f;
    [SerializeField]
    protected float StarScaleFar = 0.85f;

    void Start()
    {
        LoadTexts();
        LoadStars();
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

        sbyte numGood = gm.LastNumCorrectAnswers;
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

        sbyte numGood = gm.LastNumCorrectAnswers;
        sbyte total = gm.LastNumExcercises;
        if (total == 0) return;

        sbyte exercisesPerStar = total >= NumMaxStars ? (sbyte)(total / NumMaxStars) : total;
        sbyte currentStars = (sbyte)Mathf.CeilToInt(numGood * 1.0f / exercisesPerStar * 1.0f);
        sbyte halfStars = (sbyte)Mathf.CeilToInt(currentStars / 2.0f);
        Debug.Log("CompletedActivity:LoadStars [eps" + exercisesPerStar + "] [ns" + currentStars + "] [hs"+ halfStars + "]");

        if (currentStars % 2 == 0)
        {
            // 1  .2.  .3.  4    |  4/2 = 2
            float halfDec = (halfStars * 2 + 1) / 2.0f;
            for (sbyte i = 1; i <= currentStars; i++)
            {
                var star = Instantiate(StarPrefab, StarsLayout);
                if (Mathf.Abs(i - halfDec) > 0.6f)
                {
                    bool closeToHalf = Mathf.Abs(i - halfDec) <= 1.1f;
                    RectTransform rt = star.GetComponent<RectTransform>();
                    rt.sizeDelta *= (closeToHalf ? StarScaleClose : StarScaleFar);
                }
            }
        }
        else
        {
            // 1  .2.  >3<  .4.  5    |  5/2 = 2.5...3
            for (sbyte i = 1; i <= currentStars; i++)
            {
                var star = Instantiate(StarPrefab, StarsLayout);
                if (i != halfStars)
                {
                    int aux = i - halfStars;
                    bool closeToHalf = Mathf.Abs(aux) == 1;
                    RectTransform rt = star.GetComponent<RectTransform>();
                    rt.sizeDelta *= (closeToHalf ? StarScaleClose : StarScaleFar);
                }
            }
        }

        if (gm != null && gm.IsPlayerActive())
        {
            // Validate
            if (gm.currentPlayer.StarsWon < uint.MaxValue - (uint)currentStars)
            {
                gm.currentPlayer.StarsWon += (uint)currentStars;
                DBManager.Instance.UpdatePlayerToDb(gm.currentPlayer);
            }
        }
    }
}
