using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContextActivity : BaseActivity
{
    [SerializeField]
    private TMP_Text ExerciseText;
    [SerializeField]
    private Character Speecher;
    [SerializeField]
    private CharacterExpressions Expressions;

    protected override void LoadCurrentEmotion()
    {
        base.LoadCurrentEmotion();

        if (Exercises == null) return;
        EmotionContext ctx = (EmotionContext) Exercises.Photos[CurrentExercise];
        if (ctx == null) return;

        if (ExerciseText != null)
        {
            //var gm = GameManager.Instance;
            //var contextStrings = gm.Emotions[ExerciseEmotion].Contexts;
            //ExerciseText.text = contextStrings[Random.Range(0, contextStrings.Count)];
            ExerciseText.text = ctx.Text;
        }

        if (Speecher != null)
        {
            Speecher.ChangeCustomization(ctx.RndCharacter);
        }
        if (Expressions != null)
        {
            Expressions.PlayEmotion(ctx.PhotoEmotion);
        }
    }


}
