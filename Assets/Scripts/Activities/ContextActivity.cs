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

    protected override void LoadExerciseDataBD()
    {
        EmotionPhoto photo = Exercises.Photos[CurrentExercise];
        if (photo == null) return;

        EmotionContext context = (EmotionContext)photo;
        if (context == null) return;

        //TODO: Needs rework
        Exercise exercise = new Exercise();

        var gm = GameManager.Instance;
        ExerciseContent.IdStruct contentId = new ExerciseContent.IdStruct();
        contentId.emotion = ExerciseEmotion;
        contentId.type = ExerciseContent.EValueType.Text;
        var sprites = gm.Emotions[ExerciseEmotion].ExerciseContents.Contexts;
        contentId.order = sprites.FindIndex((x) => x == context.Text);

        exercise.ActivityId = Activity;
        exercise.ContentId = contentId.ToString();
        exercise.Id = DBManager.Instance.FindOrAddExerciseIdToDb(exercise);

        Debug.LogWarning("BaseActivity:LoadExerciseDataBD " + exercise.ContentId);
        CurrentExerciseDBO = exercise;
    }
}
