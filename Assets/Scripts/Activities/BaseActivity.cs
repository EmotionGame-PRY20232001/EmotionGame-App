using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseActivity : EmotionExercise
{
    private static BaseActivity _instance;
    public static BaseActivity Instance {  get { return _instance; } }


    [SerializeField]
    protected Exercise.EActivity Activity;
    [SerializeField]
    private TMP_Text ScoreText;
    [SerializeField] 
    private int NumButtons = 4;
    [SerializeField]
    private int Score;

    [SerializeField]
    protected GameObject AreaOfButtons;
    [SerializeField]
    protected EmotionButton BtnEmotionPrefab;

    protected readonly int GoodScore = 10;
    protected readonly int BadScore = 5;

    private List<GameObject> InstantiateButtons = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        CurrentExercise = -1;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        SetEmotionExercises();
        UpdateScoreText();
        LoadExercise();
    }

    private void CleanUp()
    {
        foreach (var go in InstantiateButtons)
            Destroy(go);
    }

    public void Good()
    {
        Score += GoodScore;
        UpdateScoreText();
        LoadExercise();
    }
    
    public void Bad()
    {
        if (Score >= BadScore) Score -= BadScore;
        UpdateScoreText();
        LoadExercise();
    }

    protected virtual void LoadExercise()
    {
        if (CurrentExercise == NumExcercises - 1)
        {
            UIActions.GoToGameComplete();
            return;
        }

        LoadCurrentExercise(CurrentExercise + 1);

        LoadEmotionButtons();
    }

    protected override void LoadExercise(EmotionPhoto photo)
    {
        base.LoadExercise(photo);
        photo?.gameObject.SetActive(false);
    }

    protected virtual void EnableCurrentExercise (bool value)
    {
        if (Exercises == null || CurrentExercise < 0) return;

        if (Exercises.Photos.Count > CurrentExercise &&
            Exercises.Photos[CurrentExercise] != null)
        {
            Exercises.Photos[CurrentExercise].gameObject.SetActive(value);
        }
    }

    protected virtual void LoadCurrentExercise(int newCurrent)
    {
        EnableCurrentExercise(false);
        CurrentExercise = newCurrent;
        EnableCurrentExercise(true);
        LoadCurrentEmotion();
    }

    protected virtual void LoadEmotionButtons()
    {
        CleanUp();

        var gm = GameManager.Instance;
        var allEmotions = new List<Exercise.EEmotion>(gm.AllEmotions);
        allEmotions.Remove(ExerciseEmotion);
        allEmotions = allEmotions.OrderBy(x => Random.value).ToList();

        var selEmotions = allEmotions.GetRange(0, NumButtons - 1);
        selEmotions.Add(ExerciseEmotion);
        selEmotions = selEmotions.OrderBy(x => Random.value).ToList();
        
        foreach (Exercise.EEmotion emotion in selEmotions)
        {
            var button = Instantiate(BtnEmotionPrefab, AreaOfButtons.transform);
            button.SetEmotion(emotion);
            InstantiateButtons.Add(button.gameObject);
        }
    }

    private void UpdateScoreText()
    {
        ScoreText.text = "Score: " + Score;
    }
}
